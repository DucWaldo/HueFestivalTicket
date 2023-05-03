using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task InsertRoleAsync(RoleDTO role)
        {
            var newRole = _mapper.Map<Role>(role);
            await _dbSet.AddAsync(newRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(string name)
        {
            var role = await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
            if (role != null)
            {
                _dbSet.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _dbSet.ToListAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }

        public async Task<RoleDTO> GetRoleByNameAsync(string name)
        {
            var roles = await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
            return _mapper.Map<RoleDTO>(roles);
        }

        public async Task UpdateRoleAsync(string oldRole, RoleDTO role)
        {
            var existingRole = await _dbSet.FirstOrDefaultAsync(r => r.Name == oldRole);
            _mapper.Map(role, existingRole);
            if (existingRole != null)
            {
                _dbSet.Update(existingRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
