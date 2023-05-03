using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<List<Event>> GetAllEventsAsync();
        public Task<EventDTO> GetEventByIdAsync(Guid Id);
        public Task<EventDTO> GetEventByNameAsync(string name);
        public Task InsertEventAsync(EventDTO @event);
        public Task UpdateEventAsync(Guid Id, EventDTO newEvent);
        public Task DeleteEventAsync(Guid Id);
    }
}
