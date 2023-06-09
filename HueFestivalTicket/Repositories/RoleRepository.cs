﻿using AutoMapper;
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

        public RoleRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public async Task InsertRoleAsync(RoleDTO role)
        {
            var newRole = _mapper.Map<Role>(role);
            await InsertAsync(newRole);
        }

        public async Task DeleteRoleAsync(string name)
        {
            var role = await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
            if (role != null)
            {
                await DeleteAsync(role);
            }
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await GetAllAsync();
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
                await UpdateAsync(existingRole);
            }
        }

        public async Task<Guid> GetIdRoleByNameAsync(string name)
        {
            var roles = await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
            if (roles != null)
            {
                return roles.IdRole;
            }
            else
            {
                return Guid.Empty;
            }

        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(r => r.IdRole == id);
            return result;
        }
    }
}
