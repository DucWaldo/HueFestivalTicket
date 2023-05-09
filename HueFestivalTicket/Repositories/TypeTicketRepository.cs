using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class TypeTicketRepository : RepositoryBase<TypeTicket>, ITypeTicketRepository
    {
        public TypeTicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckNameTypeTicketAsync(Guid id, string name)
        {
            var result = await _dbSet.FirstOrDefaultAsync(tt => tt.Name == name && tt.IdTypeTicket != id);
            if (result != null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteTypeTicketAsync(TypeTicket typeTicket)
        {
            await DeleteAsync(typeTicket);
        }

        public async Task<List<TypeTicket>> GetAllTypeTicketAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TypeTicket?> GetTypeTicketByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(tt => tt.IdTypeTicket == id);
            return result;
        }

        public async Task<TypeTicket?> GetTypeTicketByNameAsync(string name)
        {
            var result = await _dbSet.FirstOrDefaultAsync(tt => tt.Name == name);
            return result;
        }

        public async Task<TypeTicket> InsertTypeTicketAsync(TypeTicketDTO typeTicket)
        {
            var newTypeTicket = new TypeTicket
            {
                Name = typeTicket.Name
            };
            await InsertAsync(newTypeTicket);
            return newTypeTicket;
        }

        public async Task UpdateTypeTicketAsync(TypeTicket oldTypeTicket, TypeTicketDTO newTypeTicket)
        {
            oldTypeTicket.Name = newTypeTicket.Name;
            await UpdateAsync(oldTypeTicket);
        }
    }
}
