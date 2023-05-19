using HueFestivalTicket.Middlewares;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITokenRepository : IRepository<ManagerToken>
    {
        public Task<Token> InsertTokenAsync(Account account, Role role);
        public Task<ManagerToken?> GetTokenAsync(string refreshToken);
        public Task UpdateTokenAsync(ManagerToken token);
    }

    public interface IVerifyRepository : IRepository<ManagerVerify>
    {
        public Task InsertVerifyAsync(string phoneNumber, string code);
        public Task<bool> CheckVerifyCodeAsync(string phoneNumber, string code);
        public Task UpdateVerifyCodeAsync(string phoneNumber, string code);
    }
}
