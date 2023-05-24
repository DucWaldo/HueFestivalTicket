using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Helpers.EmailBuilder;
using HueFestivalTicket.Helpers.Payment;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITicketRepository _ticketRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPriceTicketRepository _priceTicketRepository;
        private readonly IEventLocationRepository _eventLocationRepository;
        private readonly ITypeTicketRepository _typeTicketRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly EmailBuilderWithCloudinary _emailBuilder;
        private readonly IWebHostEnvironment _environment;
        private readonly IPaymentRepository _paymentRepository;


        public TicketsController(ApplicationDbContext context,
            ITicketRepository ticketRepository,
            IInvoiceRepository invoiceRepository,
            IPriceTicketRepository priceTicketRepository,
            IEventLocationRepository eventLocationRepository,
            ITypeTicketRepository typeTicketRepository,
            ICustomerRepository customerRepository,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            IPaymentRepository paymentRepository)
        {
            _context = context;
            _ticketRepository = ticketRepository;
            _invoiceRepository = invoiceRepository;
            _priceTicketRepository = priceTicketRepository;
            _eventLocationRepository = eventLocationRepository;
            _typeTicketRepository = typeTicketRepository;
            _customerRepository = customerRepository;
            _configuration = configuration;
            _environment = environment;
            _emailBuilder = new EmailBuilderWithCloudinary(_configuration, _environment);
            _paymentRepository = paymentRepository;
        }

        // GET: api/Tickets
        [HttpGet]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        // GET: api/Tickets/Paging
        [HttpGet("Paging")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketPaging(int pageNumber, int pageSize)
        {
            var result = await _ticketRepository.GetTicketPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Tickets/5
        [HttpGet("{numberTicket}")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<Ticket>> GetTicket(string numberTicket)
        {
            var ticket = await _ticketRepository.GetTicketByTicketNumberAsync(numberTicket);

            if (ticket == null)
            {
                return Ok(new
                {
                    Message = "This Ticket doesn't exist"
                });
            }

            return ticket;
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<Ticket>> PostTicket([FromForm] TicketDTO ticket)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(ticket.IdEventLocation);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }
            if (eventLocation.DateEnd < DateTime.Today || eventLocation.Status == false)
            {
                return Ok(new
                {
                    Message = "This Event Location has ended"
                });
            }
            if (eventLocation.Price == 0)
            {
                return Ok(new
                {
                    Message = "This Event Location is a free event, no booking required"
                });
            }
            var typeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(ticket.IdTypeTicket);
            if (typeTicket == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(ticket.IdCustomer);
            if (customer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }
            var priceTicket = await _priceTicketRepository.GetPriceTicketByIdEventLocationAndIdTypeTicketAsync(ticket.IdEventLocation, ticket.IdTypeTicket);
            if (priceTicket == null)
            {
                return Ok(new
                {
                    Message = "This Price Ticket doesn't exist"
                });
            }
            var ticketQuantity = await _ticketRepository.GetNumberSlot(ticket.IdEventLocation, ticket.IdTypeTicket);

            if (ticketQuantity + ticket.Number > priceTicket.NumberSlot)
            {
                return Ok(new
                {
                    Message = $"The number of slot of the event location is now only {priceTicket.NumberSlot - ticketQuantity} slot"
                });
            }

            var newInvoice = new InvoiceDTO
            {
                IdCustomer = customer.IdCustomer,
                Total = priceTicket.Price * ticket.Number
            };
            var invoice = await _invoiceRepository.InsertInvoiceAsync(newInvoice);

            List<Ticket> list = new List<Ticket>();
            foreach (var item in Enumerable.Range(1, ticket.Number))
            {
                var result = await _ticketRepository.InsertTicketAsync(ticket, invoice, eventLocation, typeTicket, priceTicket);
                list.Add(result);
            }

            SendEmail(invoice.Customer!.Email!, list);
            return Ok(new
            {
                Message = "Insert Success",
                list
            });
        }

        // POST: api/Tickets/BeforePayment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("BookTicket")]
        public async Task<ActionResult<Ticket>> PostBookTicketPayment([FromForm] TicketDTO ticket)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(ticket.IdEventLocation);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }
            if (eventLocation.DateEnd < DateTime.Today || eventLocation.Status == false)
            {
                return Ok(new
                {
                    Message = "This Event Location has ended"
                });
            }
            if (eventLocation.Price == 0)
            {
                return Ok(new
                {
                    Message = "This Event Location is a free event, no booking required"
                });
            }
            var typeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(ticket.IdTypeTicket);
            if (typeTicket == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(ticket.IdCustomer);
            if (customer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }
            var priceTicket = await _priceTicketRepository.GetPriceTicketByIdEventLocationAndIdTypeTicketAsync(ticket.IdEventLocation, ticket.IdTypeTicket);
            if (priceTicket == null)
            {
                return Ok(new
                {
                    Message = "This Price Ticket doesn't exist"
                });
            }
            var ticketQuantity = await _ticketRepository.GetNumberSlot(ticket.IdEventLocation, ticket.IdTypeTicket);

            if (ticketQuantity + ticket.Number > priceTicket.NumberSlot)
            {
                return Ok(new
                {
                    Message = $"The number of slot of the event location is now only {priceTicket.NumberSlot - ticketQuantity} slot"
                });
            }

            //var newInvoice = new InvoiceDTO
            //{
            //IdCustomer = customer.IdCustomer,
            //Total = priceTicket.Price * ticket.Number
            //};
            //var invoice = await _invoiceRepository.InsertInvoiceAsync(newInvoice);
            //var payment = _paymentRepository.Payment(invoice.IdInvoice, invoice.Total, ticket);
            var payment = _paymentRepository.Payment(Guid.NewGuid(), priceTicket.Price * ticket.Number, ticket);
            return Ok(new
            {
                Message = payment
            });
        }

        [HttpGet("AfterPayment")]
        public async Task<ActionResult> PaymentReturnAsync()
        {
            if (Request.QueryString.ToString().Length > 0)
            {
                string hashSecret = _configuration["VnPay:HashSecret"] ?? ""; //Chuỗi bí mật
                var vnpayData = Request.Query;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                var queryParameters = vnpayData.ToDictionary(x => x.Key, x => x.Value.ToString());
                foreach (var s in queryParameters)
                {
                    string key = s.Key;
                    string value = s.Value;
                    if (!string.IsNullOrEmpty(value) && key.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(key, value);
                    }
                }

                string orderId = pay.GetResponseData("vnp_TxnRef"); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.Query["vnp_SecureHash"].FirstOrDefault()!; //hash của dữ liệu trả về
                string[] vnp_OrderInfo = pay.GetResponseData("vnp_OrderInfo").Split("|");
                string vnp_Amount = pay.GetResponseData("vnp_Amount");
                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        var ticket = new TicketDTO
                        {
                            IdEventLocation = Guid.Parse(vnp_OrderInfo[0]),
                            IdTypeTicket = Guid.Parse(vnp_OrderInfo[1]),
                            IdCustomer = Guid.Parse(vnp_OrderInfo[2]),
                            Number = int.Parse(vnp_OrderInfo[3]),
                        };
                        var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(ticket.IdEventLocation);
                        var typeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(ticket.IdTypeTicket);
                        var priceTicket = await _priceTicketRepository.GetPriceTicketByIdEventLocationAndIdTypeTicketAsync(ticket.IdEventLocation, ticket.IdTypeTicket);
                        //

                        var newInvoice = new InvoiceDTO
                        {
                            IdInvoice = Guid.Parse(orderId),
                            IdCustomer = ticket.IdCustomer,
                            Total = decimal.Parse(vnp_Amount) / 100
                        };
                        var invoice = await _invoiceRepository.InsertInvoiceAsync(newInvoice);
                        var invoices = await _invoiceRepository.GetInvoiceByIdAsync(Guid.Parse(orderId));
                        List<Ticket> list = new List<Ticket>();
                        foreach (var item in Enumerable.Range(1, int.Parse(vnp_OrderInfo[3])))
                        {
                            var result = await _ticketRepository.InsertTicketAsync(ticket, invoice!, eventLocation!, typeTicket!, priceTicket!);
                            list.Add(result);
                        }

                        SendEmail(invoices!.Customer!.Email!, list);
                        return Ok(new
                        {
                            Message = $"Thanh toán thành công hoá đơn {orderId}, vui lòng truy cập vào email {invoices.Customer.Email} của bạn để nhận vé",
                            list
                        });
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        return Ok(new
                        {
                            Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Có lỗi xảy ra trong quá trình xử lý"
                    });
                }
            }

            return Ok(new
            {
                Message = "Vui lòng đặt vé và thanh toán"
            });
        }


        // GET: api/Tickets/EventLocation
        [HttpGet("GetSlot")]
        public async Task<ActionResult<Ticket>> GetSlotTicket(Guid idEventLocation)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(idEventLocation);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }

            string message = string.Empty;
            var slot = await _priceTicketRepository.GetNumberSlotPriceTicketAsync(eventLocation.IdEventLocation);
            foreach (var item in slot)
            {
                var numberSlot = await _ticketRepository.GetNumberSlot(item.IdEventLocation, item.IdTypeTicket);
                message += $"There are already {numberSlot} slot {item.TypeTicket!.Name}, {item.NumberSlot - numberSlot} slot left." + "\n";
            }
            Console.WriteLine(message);
            return Ok(new
            {
                //Message = $"Có {numberSlot} và còn {slot!.NumberSlot - numberSlot}"
                Message = message
            });
        }

        private void SendEmail(string recipient, List<Ticket> tickets)
        {
            var host = _configuration["Mail:Host"];
            var name = _configuration["Mail:Name"];
            var email = _configuration["Mail:Email"];
            var password = _configuration["Mail:Password"];
            var subject = _configuration["Mail:Subject"];

            //var webRootPath = _environment.WebRootPath;
            //var filePath = webRootPath + "\\images\\26a674b4-a776-44d2-9d09-35e1f989a879.jpg";


            //string htmlBody = _emailBuilderTest.BuildEmailContent(tickets);
            string htmlBody = _emailBuilder.BuildEmailContent(tickets);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, email));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlBody;
            foreach (var ticket in tickets)
            {
                var filePath = _emailBuilder.GenerateQRCodeImage(ticket.QRCode!, ticket.TicketNumber!);
                bodyBuilder.Attachments.Add(filePath);
                _emailBuilder.DeleteFile(filePath);
            }


            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                //client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                //client.Authenticate("gordon.corwin73@ethereal.email", "k15ZCfW2zUztsSyVbc");

                client.Connect(host, 587, SecureSocketOptions.StartTls);
                client.Authenticate(email, password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
