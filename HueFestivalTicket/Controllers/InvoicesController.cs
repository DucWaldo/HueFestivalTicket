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

        public InvoicesController(ApplicationDbContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _invoiceRepository.GetAllInvoiceAsync();
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
