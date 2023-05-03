using HueFestivalTicket.Data;
using HueFestivalTicket.Data.Repository;
using HueFestivalTicket.Models;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<List<RoleDTO>> GetAllRolesAsync();
        public Task<RoleDTO> GetRoleByNameAsync(string name);
        public Task InsertRoleAsync(RoleDTO role);
        public Task UpdateRoleAsync(string oldRole, RoleDTO role);
        public Task DeleteRoleAsync(string name);
    }
}
