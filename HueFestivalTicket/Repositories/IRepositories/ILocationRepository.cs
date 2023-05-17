using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        public Task<List<Location>> GetAllLocationsAsync();
        public Task<object> GetLocationPagingAsync(int pageNumber, int pageSize);
        public Task<Location?> GetLocationByIdAsync(Guid id);
        public Task<Location> InsertLocationAsync(LocationDTO location);
        public Task UpdateLocationAsync(Location oldLocation, LocationDTO newLocation);
        public Task DeleteLocationAsync(Location location);
        public bool CheckImage(IFormFile? file);
    }
}
