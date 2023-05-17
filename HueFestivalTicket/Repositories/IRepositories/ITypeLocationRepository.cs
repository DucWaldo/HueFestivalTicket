using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITypeLocationRepository : IRepository<TypeLocation>
    {
        public Task<List<TypeLocation>> GetAllTypeLocationAsync();
        public Task<object> GetTypeLocationPagingAsync(int pageNumber, int pageSize);
        public Task<TypeLocation?> GetTypeLocationByIdAsync(Guid id);
        public Task<TypeLocation?> GetTypeLocationByNameAsync(string name);
        public Task<TypeLocation> InsertTypeLocationAsync(TypeLocationDTO typeLocation);
        public Task UpdateTypeLocationAsync(TypeLocation oldTypeLocation, TypeLocationDTO newTypeLocation);
        public Task DeleteTypeLocationAsync(TypeLocation typeLocation);
        public Task<bool> CheckNameTypeLocation(string name, Guid id);
    }
}
