using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        public Task<List<Invoice>> GetAllInvoiceAsync();
        public Task<object> GetInvoicePagingAsync(int pageNumber, int pageSize);
        public Task<Invoice?> GetInvoiceByIdAsync(Guid id);
        public Task<List<Invoice>> GetInvoiceByIdCustomerAsync(Customer customer);
        public Task<Invoice> InsertInvoiceAsync(InvoiceDTO invoice);
        public Task UpdateInvoiceAsync(Invoice oldInvoice, InvoiceDTO newInvoice);
        public Task DeleteInvoiceAsync(Invoice invoice);
    }
}
