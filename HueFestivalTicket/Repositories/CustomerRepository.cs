using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HueFestivalTicket.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IMapper _mapper;
        public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> CheckIdCardCustomerAsync(Guid id, string idCard)
        {
            var customer = await _dbSet.FirstOrDefaultAsync(c => c.IdCard == idCard && c.IdCustomer != id);
            if (customer != null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            await DeleteAsync(customer);
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(c => c.IdCustomer == id);
            return result;
        }

        public async Task<Customer?> GetCustomerByIdCardAsync(string idCard)
        {
            var result = await _dbSet.FirstOrDefaultAsync(c => c.IdCard == idCard);
            return result;
        }

        public async Task<Customer> InsertCustomerAsync(CustomerDTO customer)
        {
            var newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IdCard = customer.IdCard
            };
            await InsertAsync(newCustomer);
            return newCustomer;
        }

        public bool IsEmail(string email)
        {
            string emailPattern = @"^\S+@\S+\.\S+$";

            Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email ?? "");
        }

        public bool IsPhone(string phone)
        {
            var regex = new Regex(@"^(\+[0-9]{1,3})?[0-9]{10}$");
            return regex.IsMatch(phone ?? "");
        }

        public async Task UpdateCustomerAsync(Customer oldCustomer, CustomerDTO newCustomer)
        {
            _mapper.Map(newCustomer, oldCustomer);
            await UpdateAsync(oldCustomer);
        }
    }
}
