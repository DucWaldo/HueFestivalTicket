using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IEventLocationRepository : IRepository<EventLocation>
    {
        public Task<List<EventLocation>> GetAllEventLocationsAsync();
        public Task<EventLocation?> GetEventLocationByIdAsync(Guid id);
        public Task<EventLocation> InsertEventLocationAsync(EventLocationDTO eventLocation);
        public Task UpdateEventLocationAsync(EventLocation oldEventLocation, EventLocationDTO newEventLocation);
        public Task DeleteEventLocationAsync(EventLocation eventLocation);
        public Task<string> CheckDateTimeEventLocation(EventLocationDTO eventLocation);
        public Task<string> CheckDateTimeEventLocationToUpdate(Guid id, EventLocationDTO eventLocation);
    }
}
