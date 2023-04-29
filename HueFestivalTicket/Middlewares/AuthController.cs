using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HueFestivalTicket.Middlewares
{
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Accounts/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountDTO account)
        {
            var accountLogin = await _context.Accounts.SingleOrDefaultAsync(x => x.Username == account.Username && x.Password == Encrypt.GetMD5Hash(account.Password ?? ""));
            if (accountLogin == null)
            {
                return Ok(new
                {
                    Message = "Invalid username/password"
                });
            }

            var rolename = await _context.Roles.SingleOrDefaultAsync(x => x.IdRole == accountLogin.IdRole);
            if (rolename == null)
            {
                return Ok(new
                {
                    Message = $"Role with ID {accountLogin} not found."
                });
            }

            return Ok(new
            {
                token = CreateToken(accountLogin, rolename)
            });
        }

        private string CreateToken(Account account, Role role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Username ?? ""),
                    new Claim(ClaimTypes.Role, role.Name ?? "")
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
