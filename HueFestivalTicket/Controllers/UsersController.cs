using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Middlewares;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UsersController(ApplicationDbContext context, IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _userRepository.GetAllUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok(new
                {
                    Message = $"Can't find user by {id}"
                });
            }
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO user)
        {

            var users = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
            if (users == null)
            {
                return Ok(new
                {
                    Message = "This ID could not be found"
                });
            }

            if (users.PhoneNumber != user.PhoneNumber)
            {
                return Ok(new
                {
                    Message = "Phone number doesn't match"
                });
            }

            var check = await _context.Users.Where(c => c.IdUser != users.IdUser).ToListAsync();
            if (check.Any(c => c.Email == user.Email || c.PhoneNumber == user.PhoneNumber))
            {
                return Ok(new
                {
                    Message = "Email or Phone Number already exists"
                });
            }

            await _userRepository.UpdateUserAsync(user, users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var users = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber || u.Email == user.Email);
            if (users != null)
            {
                return Ok(new
                {
                    Message = "Phone Number or Email has been duplicated"
                });
            }

            var checkEmail = IsEmail(user.Email);
            var checkPhone = IsPhone(user.PhoneNumber);
            if (checkEmail == false || checkPhone == false)
            {
                return Ok(new
                {
                    Message = "Invalid Email/Phone number"
                });
            }

            var getRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (getRole == null)
            {
                return Ok(new
                {
                    Message = "Role not found"
                });
            }
            var newAccount = new Account
            {
                Username = user.PhoneNumber,
                Password = Generate.GetMD5Hash(user.PhoneNumber ?? ""),
                IsActive = true,
                TimeJoined = DateTime.UtcNow,
                IdRole = getRole.IdRole
            };
            await _accountRepository.InsertAccountAsync(newAccount);
            await _context.SaveChangesAsync();

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Organization = user.Organization,
                IdAccount = newAccount.IdAccount
            };
            await _userRepository.InsertUserAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                newUser,
                newAccount
            });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return Ok(new
                {
                    Message = "Can't find this user"
                });
            }

            var account = await _accountRepository.GetAccountByIdAsync(user.IdAccount);
            if (account == null)
            {
                return Ok(new
                {
                    Message = "Can't find this account"
                });
            }

            await _userRepository.DeleteUserAsync(user);
            await _accountRepository.DeleteAccountAsync(account);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Delete Success"
            });
        }

        private bool IsEmail(string? email)
        {
            string emailPattern = @"^\S+@\S+\.\S+$";

            Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email ?? "");
        }
        private bool IsPhone(string? phone)
        {
            var regex = new Regex(@"^(\+[0-9]{1,3})?[0-9]{10}$");
            return regex.IsMatch(phone ?? "");
        }
    }
}
