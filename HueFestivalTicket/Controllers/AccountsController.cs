using HueFestivalTicket.Contexts;
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
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public AccountsController(ApplicationDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _accountRepository.GetAllAccountAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
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

        [HttpGet("acc")]
        [Authorize]
        public IActionResult Get()
        {
            var IdAccount = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(new
            {
                IdAccount
            });
        }

        /*
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDTO account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Accounts.AnyAsync(x => x.Username == account.Username))
            {
                return BadRequest("Email already exists");
            }

            var acc = new Account
            {
                Username = account.Username,
                Password = Generate.GetMD5Hash(account.Password ?? ""),
                IsActive = true,
                TimeJoined = DateTime.UtcNow,
                IdRole = Guid.Parse("018cceb3-5aa7-4283-e3f5-08db49dc998c")
            };

            await _context.Accounts.AddAsync(acc);
            await _context.SaveChangesAsync();

            return Ok();
        }
        */
    }
}
