using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        private readonly IMapper _mapper;

        public InvoiceRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            await DeleteAsync(invoice);
        }

        public async Task<List<Invoice>> GetAllInvoiceAsync()
        {
            return await GetAllWithIncludesAsync(i => i.Customer!);
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
        {
            var result = await _dbSet.Include(i => i.Customer).FirstOrDefaultAsync(i => i.IdInvoice == id);
            return result;
        }

        public async Task<List<Invoice>> GetInvoiceByIdCustomerAsync(Customer customer)
        {
            var list = await _dbSet.Include(i => i.Customer).Where(i => i.IdCustomer == customer.IdCustomer).ToListAsync();
            return list;
        }

        public async Task<object> GetInvoicePagingAsync(int pageNumber, int pageSize)
        {
            List<Invoice> data = await GetPage(pageNumber, pageSize, i => i.TimeCreate, i => i.Customer!);
            return ReturnGetPage(data, pageNumber, pageSize);
        }

        public async Task<Invoice> InsertInvoiceAsync(InvoiceDTO invoice)
        {
            var newInvoice = new Invoice
            {
                IdInvoice = invoice.IdInvoice,
                TimeCreate = DateTime.UtcNow,
                Total = invoice.Total,
                IdCustomer = invoice.IdCustomer
            };
            await InsertAsync(newInvoice);
            return newInvoice;
        }

        public async Task UpdateInvoiceAsync(Invoice oldInvoice, InvoiceDTO newInvoice)
        {
            _mapper.Map(newInvoice, oldInvoice);
            await UpdateAsync(oldInvoice);
        }
    }
}
