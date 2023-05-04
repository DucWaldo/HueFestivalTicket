﻿using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventRepository _eventRepository;

        public EventsController(ApplicationDbContext context, IEventRepository eventRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            return await _eventRepository.GetAllEventsAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEvent(Guid id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var @event = await _eventRepository.GetEventByIdAsync(id);

            if (@event == null)
            {
                return Ok(new
                {
                    Message = $"Can't find event with {id}"
                });
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, EventDTO @event)
        {
            var check = await _eventRepository.GetEventByIdAsync(id);
            if (check == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} does not exist"
                });
            }

            await _eventRepository.UpdateEventAsync(id, @event);

            return Ok(new
            {
                Message = $"Change success"
            });
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(EventDTO @event)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }

            var eventName = await _eventRepository.GetEventByNameAsync(@event.Name ?? "");
            if (eventName != null)
            {
                return Ok(new
                {
                    Message = "Event already exists"
                });
            }
            if (@event.TypeEvent < 1 || @event.TypeEvent > 2)
            {
                return Ok(new
                {
                    Message = "Please choose 1 (Spotlight) or 2 (Community) for TypeEvent"
                });
            }


            await _eventRepository.InsertEventAsync(@event);

            return Ok(new
            {
                Message = "Create Success",
                @event
            });
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var @event = await _eventRepository.GetEventByIdAsync(id);
            if (@event == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} does not exist"
                });
            }

            await _eventRepository.DeleteEventAsync(id);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Delete Success"
            });
        }

        private bool EventExists(Guid id)
        {
            return (_context.Events?.Any(e => e.IdEvent == id)).GetValueOrDefault();
        }
    }
}