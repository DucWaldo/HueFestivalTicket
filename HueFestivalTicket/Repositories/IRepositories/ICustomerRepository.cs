using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<List<Customer>> GetAllCustomersAsync();
        public Task<object> GetCustomerPagingAsync(int pageNumber, int pageSize);
        public Task<Customer?> GetCustomerByIdAsync(Guid id);
        public Task<Customer> InsertCustomerAsync(CustomerDTO customer);
        public Task UpdateCustomerAsync(Customer oldCustomer, CustomerDTO newCustomer);
        public Task DeleteCustomerAsync(Customer customer);
        public Task<Customer?> GetCustomerByIdCardAsync(string idCard);
        public Task<bool> CheckIdCardCustomerAsync(Guid id, string idCard);
        public bool IsEmail(string email);
        public bool IsPhone(string phone);

    }
}
