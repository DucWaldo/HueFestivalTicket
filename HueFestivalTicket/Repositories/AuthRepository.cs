using HueFestivalTicket.Contexts;
using HueFestivalTicket.Middlewares;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HueFestivalTicket.Repositories
{
    public class TokenRepository : RepositoryBase<ManagerToken>, ITokenRepository
    {
        private readonly IConfiguration _configuration;
        public TokenRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        public async Task<ManagerToken?> GetTokenAsync(string refreshToken)
        {
            var result = await _dbSet.FirstOrDefaultAsync(t => t.Token == refreshToken);
            return result;
        }

        public async Task<Token> InsertTokenAsync(Account account, Role role)
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

            await InsertAsync(managerToken);

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task UpdateTokenAsync(ManagerToken token)
        {
            token.IsRevoked = true;
            token.IsUsed = true;
            await UpdateAsync(token);
        }
    }

    public class VerifyRepository : RepositoryBase<ManagerVerify>, IVerifyRepository
    {
        public VerifyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckVerifyCodeAsync(string phoneNumber, string code)
        {
            var result = await _dbSet.FirstOrDefaultAsync(vc => vc.PhoneNumber == phoneNumber && vc.Code == code);
            if (result == null)
            {
                return false;
            }
            else if (result.Status == true || (DateTime.UtcNow - result.TimeCreate).TotalMinutes > 15 || result.TimeCreate != result.TimeUpdate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task InsertVerifyAsync(string phoneNumber, string code)
        {
            var now = DateTime.UtcNow;
            var newManagerVerify = new ManagerVerify()
            {
                PhoneNumber = phoneNumber,
                Code = code,
                TimeCreate = now,
                TimeUpdate = now,
                Status = false
            };
            await InsertAsync(newManagerVerify);
        }

        public async Task UpdateVerifyCodeAsync(string phoneNumber, string code)
        {
            var managerVerify = await _dbSet.FirstOrDefaultAsync(vc => vc.PhoneNumber == phoneNumber && vc.Code == code);
            managerVerify!.Status = true;
            managerVerify.TimeUpdate = DateTime.UtcNow;

            await UpdateAsync(managerVerify);
        }
    }
}
