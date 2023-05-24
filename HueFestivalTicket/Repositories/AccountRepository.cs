using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
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

        public async Task ChangePasswordAsync(string username, string password)
        {
            var account = _dbSet.FirstOrDefault(x => x.Username == username);
            account!.Password = Generate.GetMD5Hash(password);

            await UpdateAsync(account);
        }

        public async Task<bool> CheckUsernameAsync(string username)
        {
            var account = await _dbSet.FirstOrDefaultAsync(a => a.Username == username);
            if (account == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteAccountAsync(Account account)
        {
            await DeleteAsync(account);
        }

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            var account = await _dbSet.Include(acc => acc.Role).FirstOrDefaultAsync(a => a.IdAccount == id);
            return account;
        }

        public async Task<Account?> GetAccountLoginAsync(AccountDTO account)
        {
            var result = await _dbSet.FirstOrDefaultAsync(a => a.Username == account.Username && a.Password == Generate.GetMD5Hash(account.Password ?? ""));
            return result;
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

        public async Task UpdateRoleAsync(Account account, Guid idRole)
        {
            account.IdRole = idRole;
            await UpdateAsync(account);
        }
    }
}
