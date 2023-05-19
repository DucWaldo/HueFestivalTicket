using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Helpers;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using MailKit.Net.Smtp;
using MailKit.Security;
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


        public TicketsController(ApplicationDbContext context, ITicketRepository ticketRepository, IInvoiceRepository invoiceRepository, IPriceTicketRepository priceTicketRepository, IEventLocationRepository eventLocationRepository, ITypeTicketRepository typeTicketRepository, ICustomerRepository customerRepository, IConfiguration configuration, EmailBuilderWithCloudinary emailBuilder, EmailBuilder emailBuilderTest, IWebHostEnvironment environment)
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

        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        // GET: api/Tickets/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketPaging(int pageNumber, int pageSize)
        {
            var result = await _ticketRepository.GetTicketPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Tickets/5
        [HttpGet("{numberTicket}")]
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
        /*
        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

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

        /*
        [HttpGet("Email")]
        public async Task<ActionResult<Ticket>> CheckEmail(string email)
        {
            //SendEmail(email, "Xin chào", "Test thử xem thế nào");
            return Ok(new
            {
                Message = "Send Success"
            });
        }*/

        private void SendEmail(string recipient, List<Ticket> tickets)
        {
            var host = _configuration["Mail:Host"];
            var name = _configuration["Mail:Name"];
            var email = _configuration["Mail:Email"];
            var password = _configuration["Mail:Password"];
            var subject = _configuration["Mail:Subject"];
            var webRootPath = _environment.WebRootPath;
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
