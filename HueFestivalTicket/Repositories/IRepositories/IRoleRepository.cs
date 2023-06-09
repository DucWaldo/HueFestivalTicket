﻿using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<List<RoleDTO>> GetAllRolesAsync();
        public Task<RoleDTO> GetRoleByNameAsync(string name);
        public Task<Role?> GetRoleByIdAsync(Guid id);
        public Task<Guid> GetIdRoleByNameAsync(string name);
        public Task InsertRoleAsync(RoleDTO role);
        public Task UpdateRoleAsync(string oldRole, RoleDTO role);
        public Task DeleteRoleAsync(string name);
    }
}
