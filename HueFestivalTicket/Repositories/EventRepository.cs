using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task DeleteEventAsync(Guid Id)
        {
            var events = await _dbSet.FirstOrDefaultAsync(e => e.IdEvent == Id);
            if (events != null)
            {
                await DeleteAsync(events);
            }
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var events = await GetAllAsync();
            //return _mapper.Map<List<EventDTO>>(events);
            return events;
        }

        public async Task<EventDTO> GetEventByIdAsync(Guid Id)
        {
            var events = await _dbSet.FirstOrDefaultAsync(e => e.IdEvent == Id);
            return _mapper.Map<EventDTO>(events);
        }

        public async Task<EventDTO> GetEventByNameAsync(string name)
        {
            var events = await _dbSet.FirstOrDefaultAsync(e => e.Name == name);
            return _mapper.Map<EventDTO>(events);
        }

        public async Task InsertEventAsync(EventDTO @event)
        {
            var newEvent = _mapper.Map<Event>(@event);
            await InsertAsync(newEvent);
        }

        public async Task UpdateEventAsync(Guid Id, EventDTO newEvent)
        {
            var events = await _dbSet.FirstOrDefaultAsync(e => e.IdEvent == Id);
            _mapper.Map(newEvent, events);
            if (events != null)
            {
                await UpdateAsync(events);
            }
        }
    }
}
