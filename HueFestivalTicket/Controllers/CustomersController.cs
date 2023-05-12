using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ApplicationDbContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerDTO customer)
        {
            var oldCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (oldCustomer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }

            var checkEmail = _customerRepository.IsEmail(customer.Email!);
            var checkPhone = _customerRepository.IsPhone(customer.PhoneNumber!);
            if (checkEmail == false || checkPhone == false)
            {
                return Ok(new
                {
                    Message = "Invalid Email/Phone number"
                });
            }
            if (customer.IdCard!.Length != 9 && customer.IdCard!.Length != 12)
            {
                return Ok(new
                {
                    Message = "Invalid IdCard"
                });
            }
            if (await _customerRepository.CheckIdCardCustomerAsync(id, customer.IdCard!) == false)
            {
                return Ok(new
                {
                    Message = "IdCard already exist"
                });
            }

            await _customerRepository.UpdateCustomerAsync(oldCustomer, customer);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customer)
        {
            var checkEmail = _customerRepository.IsEmail(customer.Email!);
            var checkPhone = _customerRepository.IsPhone(customer.PhoneNumber!);
            if (checkEmail == false || checkPhone == false)
            {
                return Ok(new
                {
                    Message = "Invalid Email/Phone number"
                });
            }
            if (customer.IdCard!.Length != 9 && customer.IdCard!.Length != 12)
            {
                return Ok(new
                {
                    Message = "Invalid IdCard"
                });
            }
            if (await _customerRepository.GetCustomerByIdCardAsync(customer.IdCard!) != null)
            {
                return Ok(new
                {
                    Message = "IdCard already exist"
                });
            }
            var result = await _customerRepository.InsertCustomerAsync(customer);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return Ok(new
                {
                    Message = "This Customer doesn't exist"
                });
            }
            await _customerRepository.DeleteCustomerAsync(customer);
            return Ok(new
            {
                Message = "Delete Success"
            });
        }

        private bool CustomerExists(Guid id)
        {
            return (_context.Customers?.Any(e => e.IdCustomer == id)).GetValueOrDefault();
        }
    }
}
