using HueFestivalTicket.Contexts;
using HueFestivalTicket.Middlewares;
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
            return await GetAllWithIncludesAsync(acc => acc.Role!);
        }

        public async Task<Account> InsertAccountAsync(string? account, Guid role)
        {
            var newAccount = new Account
            {
                Username = account,
                Password = Generate.GetMD5Hash(account ?? ""),
                IsActive = true,
                TimeJoined = DateTime.UtcNow,
                IdRole = role
            };
            await InsertAsync(newAccount);
            return newAccount;
        }
    }
}
