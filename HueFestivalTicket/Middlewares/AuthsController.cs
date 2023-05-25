using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HueFestivalTicket.Middlewares
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IVerifyRepository _verifyRepository;

        public AuthsController(IConfiguration configuration,
            IAccountRepository accountRepository,
            ITokenRepository tokenRepository,
            IRoleRepository roleRepository,
            IVerifyRepository verifyRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
            _verifyRepository = verifyRepository;
        }

        // POST: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AccountDTO account)
        {
            var accountLogin = await _accountRepository.GetAccountLoginAsync(account);
            if (accountLogin == null)
            {
                return Ok(new
                {
                    Message = "Invalid username/password"
                });
            }
            if (accountLogin.IsActive == false)
            {
                return Ok(new
                {
                    Message = "This Account has been locked"
                });
            }

            var rolename = await _roleRepository.GetRoleByIdAsync(accountLogin.IdRole);
            if (rolename == null)
            {
                return Ok(new
                {
                    Message = $"Role with ID {accountLogin} not found."
                });
            }

            var result = await _tokenRepository.InsertTokenAsync(accountLogin, rolename);
            return Ok(new
            {
                Message = "Login Success",
                //Token = await CreateToken(accountLogin, rolename)
                Token = result
            });
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
                var storedToken = await _tokenRepository.GetTokenAsync(tokens.RefreshToken ?? "");
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
                //storedToken.IsRevoked = true;
                //storedToken.IsUsed = true;
                //_context.Update(storedToken);
                //await _context.SaveChangesAsync();
                await _tokenRepository.UpdateTokenAsync(storedToken);

                //create new token
                var account = await _accountRepository.GetAccountByIdAsync(storedToken.IdAccount);
                if (account == null)
                {
                    return Ok(new
                    {
                        Message = "Invalid account"
                    });
                }

                var rolename = await _roleRepository.GetRoleByIdAsync(account.IdRole);
                if (rolename == null)
                {
                    return Ok(new
                    {
                        Message = "Invalid username/password"
                    });
                }

                var token = await _tokenRepository.InsertTokenAsync(account, rolename);

                return Ok(new
                {
                    Message = "Refresh token success",
                    Token = token
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

        [HttpPost("SendVerifyCode")]
        public async Task<ActionResult<ManagerVerify>> SendVerifyCode(string userName)
        {
            var account = await _accountRepository.CheckUsernameAsync(userName);
            if (account == false)
            {
                return Ok(new
                {
                    Message = "Username doesn't exist"
                });
            }
            var code = Generate.GetVerifyCode();

            await _verifyRepository.InsertVerifyAsync(userName, code);
            SendVerificationCode(userName, code);
            return Ok(new
            {
                Message = $"Sent verification code to phone number {userName}"
            });
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string userName, string newPassword, string verifyCode)
        {
            var account = await _accountRepository.CheckUsernameAsync(userName);
            if (account == false)
            {
                return Ok(new
                {
                    Message = "Username doesn't exist"
                });
            }
            var checkVerifyCode = await _verifyRepository.CheckVerifyCodeAsync(userName, verifyCode);
            if (checkVerifyCode == false)
            {
                return Ok(new
                {
                    Message = "Verify code is incorrect or expired"
                });
            }
            await _verifyRepository.UpdateVerifyCodeAsync(userName, verifyCode);
            await _accountRepository.ChangePasswordAsync(userName, newPassword);
            return Ok(new
            {
                Message = "Change Password Success"
            });
        }

        private void SendVerificationCode(string phoneNumber, string resetCode)
        {
            var accountSid = _configuration["Twilio:accountSid"];
            var authToken = _configuration["Twilio:authToken"]!.Split("|");
            var twilioPhoneNumber = _configuration["Twilio:twilioPhoneNumber"];

            TwilioClient.Init(accountSid, authToken[0] + authToken[2]);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(Generate.GetPhoneNumber(phoneNumber)));
            messageOptions.From = new PhoneNumber(twilioPhoneNumber);
            messageOptions.Body = $"Mã xác thực của bạn là: {resetCode}";

            // Gửi tin nhắn chứa mã xác thực qua Twilio
            var message = MessageResource.Create(messageOptions);
        }
    }
}
