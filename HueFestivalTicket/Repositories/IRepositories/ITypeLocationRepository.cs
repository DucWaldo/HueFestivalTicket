using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITypeLocationRepository : IRepository<TypeLocation>
    {
        public Task<List<TypeLocation>> GetAllTypeLocationAsync();
        public Task<TypeLocation?> GetTypeLocationByIdAsync(Guid id);
        public Task<TypeLocation> GetTypeLocationByNameAsync(string name);
        public Task InsertTypeLocationAsync(TypeLocation typeLocation);
        public Task UpdateTypeLocationAsync(TypeLocation typeLocation);
        public Task DeleteTypeLocationAsync(TypeLocation typeLocation);
    }
}
