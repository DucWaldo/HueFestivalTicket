using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task DeleteUserAsync(User user)
        {
            await DeleteAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await GetAllWithIncludesAsync(u => u.Account!.Role!);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.IdUser == id);
            return user;
        }

        public async Task<User> InsertUserAsync(UserDTO user, Guid idAccount)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Organization = user.Organization,
                IdAccount = idAccount
            };
            await InsertAsync(newUser);
            return newUser;
        }

        public async Task UpdateUserAsync(UserDTO user, User users)
        {
            _mapper.Map(user, users);
            await UpdateAsync(users);
        }
    }
}
