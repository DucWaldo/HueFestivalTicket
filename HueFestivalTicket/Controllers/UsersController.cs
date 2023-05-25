using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOrManager")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;

        public UsersController(IUserRepository userRepository, IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return Ok(new
                {
                    Message = $"Can't find user"
                });
            }
            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO user)
        {
            var users = await _userRepository.GetUserByIdAsync(id);
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

            var check = await _userRepository.GetUserToCheckAsync(users.IdUser);
            if (check.Any(c => c.Email == user.Email || c.PhoneNumber == user.PhoneNumber))
            {
                return Ok(new
                {
                    Message = "Email or Phone Number already exists"
                });
            }

            await _userRepository.UpdateUserAsync(user, users);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO user)
        {
            var users = await _userRepository.CheckPhoneAndEmail(user.PhoneNumber ?? "", user.Email ?? "");
            if (users == true)
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

            var getRole = await _roleRepository.GetIdRoleByNameAsync("Staff");
            if (getRole == Guid.Empty)
            {
                return Ok(new
                {
                    Message = "Role not found"
                });
            }
            var accountResult = await _accountRepository.InsertAccountAsync(user.PhoneNumber, getRole);
            var userResult = await _userRepository.InsertUserAsync(user, accountResult.IdAccount);

            return Ok(new
            {
                Message = "Insert Success",
                userResult
            });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
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
