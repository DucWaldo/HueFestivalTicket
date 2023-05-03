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
                _dbSet.Remove(events);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var events = await _dbSet.ToListAsync();
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
            /*var newEvent = new Event()
            {
                IdEvent = Guid.NewGuid(),
                Name = @event.Name,
                Content = @event.Content,
                StatusTicket = @event.StatusTicket,
                TypeEvent = @event.TypeEvent
            };*/
            await _dbSet.AddAsync(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Guid Id, EventDTO newEvent)
        {
            var events = await _dbSet.FirstOrDefaultAsync(e => e.IdEvent == Id);
            _mapper.Map(newEvent, events);
            if (events != null)
            {
                _dbSet.Update(events);
                await _context.SaveChangesAsync();
            }
        }
    }
}
