using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        // GET: api/Events/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventPaging(int pageNumber, int pageSize)
        {
            var result = await _eventRepository.GetEventPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _eventRepository.GetEventByIdAsync(id);

            if (@event == null)
            {
                return Ok(new
                {
                    Message = "This Event not found"
                });
            }

            return @event;
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> PutEvent(Guid id, EventDTO newEvent)
        {
            var oldEvent = await _eventRepository.GetEventByIdAsync(id);
            if (oldEvent == null)
            {
                return Ok(new
                {
                    Message = "This Event not found"
                });
            }

            if (newEvent.TypeEvent < 1 || newEvent.TypeEvent > 2)
            {
                return Ok(new
                {
                    Message = "Please choose 1 (Spotlight) or 2 (Community) for TypeEvent"
                });
            }

            await _eventRepository.UpdateEventAsync(oldEvent, newEvent);

            return Ok(new
            {
                Message = "Update success"
            });
        }

        // POST: api/Events
        [HttpPost]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult<Event>> PostEvent(EventDTO @event)
        {
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
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _eventRepository.GetEventByIdAsync(id);
            if (@event == null)
            {
                return Ok(new
                {
                    Message = "This Event not found"
                });
            }

            await _eventRepository.DeleteEventAsync(id);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
