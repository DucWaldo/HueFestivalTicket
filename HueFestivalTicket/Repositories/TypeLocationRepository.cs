using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class TypeLocationRepository : RepositoryBase<TypeLocation>, ITypeLocationRepository
    {
        public TypeLocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteTypeLocationAsync(TypeLocation typeLocation)
        {
            await DeleteAsync(typeLocation);
        }

        public async Task<List<TypeLocation>> GetAllTypeLocationAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TypeLocation?> GetTypeLocationByIdAsync(Guid id)
        {
            var typeLocation = await _dbSet.FirstOrDefaultAsync(tl => tl.IdTypeLocation == id);
            return typeLocation;
        }

        public Task<TypeLocation> GetTypeLocationByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task InsertTypeLocationAsync(TypeLocation typeLocation)
        {
            await InsertAsync(typeLocation);
        }

        public async Task UpdateTypeLocationAsync(TypeLocation typeLocation)
        {
            await UpdateAsync(typeLocation);
        }
    }
}
