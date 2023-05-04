using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<List<Account>> GetAllAccountAsync();
        public Task<Account?> GetAccountByIdAsync(Guid id);
        public Task InsertAccountAsync(Account account);
        public Task DeleteAccountAsync(Account account);
    }
}
