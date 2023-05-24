using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByIdAccountAsync(Guid id);
        public Task<User> InsertUserAsync(UserDTO user, Guid idAccount);
        public Task UpdateUserAsync(UserDTO user, User users);
        public Task DeleteUserAsync(User user);
    }
}
