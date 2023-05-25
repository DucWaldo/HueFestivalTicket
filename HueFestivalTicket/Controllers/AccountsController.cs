using HueFestivalTicket.Middlewares;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IVerifyRepository _verifyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public AccountsController(IAccountRepository accountRepository,
            IVerifyRepository verifyRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _verifyRepository = verifyRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        // GET: api/Accounts
        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _accountRepository.GetAllAccountAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<Account>> GetAccount(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);

            if (account == null)
            {
                return Ok(new
                {
                    Message = "This Account not found"
                });
            }

            return account;
        }

        // PUT: api/Accounts/UpdateRole
        [HttpPut("UpdateRole")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateRole(Guid idAccount, string roleName)
        {
            var idRole = await _roleRepository.GetIdRoleByNameAsync(roleName);
            if (idRole == Guid.Empty)
            {
                return Ok(new
                {
                    Message = "This role doesn't exist"
                });
            }
            var account = await _accountRepository.GetAccountByIdAsync(idAccount);
            if (account == null)
            {
                return Ok(new
                {
                    Message = "This account doesn't exist"
                });
            }

            if (idRole == account.IdRole)
            {
                return Ok(new
                {
                    Message = "Roles have not changed"
                });
            }

            await _accountRepository.UpdateRoleAsync(account, idRole);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // PUT: api/Accounts/ChangePassword
        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<ActionResult<Account>> ChangePassword(string oldPassword, string newPassword, string verifyCode)
        {
            var acc = await _accountRepository.GetAccountByIdAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!));
            if (acc == null || acc.Password != Generate.GetMD5Hash(oldPassword))
            {
                return Ok(new
                {
                    Message = "Old password is incorrect"
                });
            }
            var checkVerifyCode = await _verifyRepository.CheckVerifyCodeAsync(acc.Username!, verifyCode);
            if (checkVerifyCode == false)
            {
                return Ok(new
                {
                    Message = "Verify code is incorrect or expired"
                });
            }
            await _verifyRepository.UpdateVerifyCodeAsync(acc.Username!, verifyCode);
            await _accountRepository.ChangePasswordAsync(acc.Username!, newPassword);
            return Ok(new
            {
                Message = "Change Password Success"
            });
        }

        [HttpGet("AccountToken")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var IdAccount = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (IdAccount == null)
            {
                return Ok(new
                {
                    Message = "This Token Invalid"
                });
            }
            var account = await _accountRepository.GetAccountByIdAsync(Guid.Parse(IdAccount));
            if (account == null)
            {
                return Ok(new
                {
                    Message = "This Account doesn't exist"
                });
            }
            var user = await _userRepository.GetUserByIdAccountAsync(account.IdAccount);
            return Ok(new
            {
                user
            });
        }
    }
}
