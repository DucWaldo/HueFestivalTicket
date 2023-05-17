using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;

        public InvoicesController(ApplicationDbContext context, IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _invoiceRepository.GetAllInvoiceAsync();
        }

        // GET: api/Invoices/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicePaging(int pageNumber, int pageSize)
        {
            var result = await _invoiceRepository.GetInvoicePagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return Ok(new
                {
                    Message = "This Invoice doesn't exist"
                });
            }

            return invoice;
        }

        // GET: api/Invoices/5
        [HttpGet("GetByCustomerIdCard")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoiceCustomer(string idCard)
        {
            var customer = await _customerRepository.GetCustomerByIdCardAsync(idCard);
            if (customer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }

            var invoice = await _invoiceRepository.GetInvoiceByIdCustomerAsync(customer);

            if (invoice.Count == 0)
            {
                return Ok(new
                {
                    Message = "This Invoice with this Customer doesn't exist"
                });
            }

            return invoice;
        }

        /*
        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, InvoiceDTO invoice)
        {
            var oldInvoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (oldInvoice == null)
            {
                return Ok(new
                {
                    Message = "This Invoice doesn't exist"
                });
            }
            await _invoiceRepository.UpdateInvoiceAsync(oldInvoice, invoice);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceDTO invoice)
        {
            var result = await _invoiceRepository.InsertInvoiceAsync(invoice);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return Ok(new
                {
                    Message = "This Invoice doesn't exist"
                });
            }

            await _invoiceRepository.DeleteInvoiceAsync(invoice);
            return Ok(new
            {
                Message = "Delete Success"
            }); ;
        }

        private bool InvoiceExists(Guid id)
        {
            return (_context.Invoices?.Any(e => e.IdInvoice == id)).GetValueOrDefault();
        }
        */
    }
}
