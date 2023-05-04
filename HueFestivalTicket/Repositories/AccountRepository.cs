using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteAccountAsync(Account account)
        {
            await DeleteAsync(account);
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            var account = await _dbSet.FirstOrDefaultAsync(a => a.IdAccount == id);
            return account;
        }

        public async Task<List<Account>> GetAllAccountAsync()
        {
            return await GetAllAsync();
        }

        public async Task InsertAccountAsync(Account account)
        {
            await InsertAsync(account);
        }
    }
}
