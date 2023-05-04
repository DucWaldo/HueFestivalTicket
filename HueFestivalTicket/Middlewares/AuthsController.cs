using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HueFestivalTicket.Middlewares
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountDTO account)
        {
            var accountLogin = await _context.Accounts.SingleOrDefaultAsync(x => x.Username == account.Username && x.Password == Generate.GetMD5Hash(account.Password ?? ""));
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
                token = await CreateToken(accountLogin, rolename)
            });
        }

        private async Task<Token> CreateToken(Account account, Role role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.IdAccount.ToString() ?? ""),
                    new Claim(ClaimTypes.Role, role.Name ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = Generate.GetRefreshToken();

            var managerToken = new ManagerToken
            {
                Token = refreshToken,
                JwtId = accessToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1),
                IdAccount = account.IdAccount
            };

            await _context.ManagerTokens.AddAsync(managerToken);
            await _context.SaveChangesAsync();

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(Token tokens)
        {
            var handler = new JwtSecurityTokenHandler();
            //var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "")),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = handler.ValidateToken(tokens.AccessToken, tokenValidateParam, out var validatedToken);

                //Check Valid Type
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                    {
                        return Ok(new
                        {
                            Message = "Invalid token"
                        });
                    }
                }

                //Check accessToken expire?
                var utcExpireDate = tokenInVerification.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                if (utcExpireDate == null)
                {
                    return Ok(new
                    {
                        Message = "Can't find Time Exp"
                    });
                }
                var expireDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(utcExpireDate));
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new
                    {
                        Message = "Access token has not yet expired"
                    });
                }

                //Check refreshtoken exist in DB
                var storedToken = _context.ManagerTokens.FirstOrDefault(x => x.Token == tokens.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new
                    {
                        Message = "Refresh token does not exist"
                    });
                }

                //Check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return Ok(new
                    {
                        Message = "Refresh token has been used"
                    });
                }
                if (storedToken.IsRevoked)
                {
                    return Ok(new
                    {
                        Message = "Refresh token has been revoked"
                    });
                }

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();

                //create new token
                var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.IdAccount == storedToken.IdAccount);
                if (account == null)
                {
                    return Ok(new
                    {
                        Message = "Invalid username/password"
                    });
                }

                var rolename = await _context.Roles.SingleOrDefaultAsync(x => x.IdRole == account.IdRole);
                if (rolename == null)
                {
                    return Ok(new
                    {
                        Message = "Invalid username/password"
                    });
                }

                var token = await CreateToken(account, rolename);

                return Ok(new
                {
                    Message = "Refresh token success",
                    Data = token
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    Message = "Something went wrong"
                });
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new
            {
                message = "Logout success"
            });
        }
    }
}
