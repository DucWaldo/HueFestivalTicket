using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEventLocationRepository _eventLocationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        public EventLocationsController(ApplicationDbContext context, IEventLocationRepository eventLocationRepository, ILocationRepository locationRepository, IEventRepository eventRepository)
        {
            _context = context;
            _eventLocationRepository = eventLocationRepository;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
        }

        // GET: api/EventLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventLocation>>> GetEventLocations()
        {
            if (_context.EventLocations == null)
            {
                return NotFound();
            }
            return await _eventLocationRepository.GetAllEventLocationsAsync();
        }

        // GET: api/EventLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventLocation>> GetEventLocation(Guid id)
        {
            if (_context.EventLocations == null)
            {
                return NotFound();
            }
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);

            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }

            return eventLocation;
        }

        // PUT: api/EventLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventLocation(Guid id, EventLocationDTO eventLocation)
        {
            var oldEventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);
            if (oldEventLocation == null)
            {
                return Ok(new
                {
                    Message = "Event Location not found"
                });
            }

            if (_eventRepository.GetEventByIdAsync(eventLocation.IdEvent) == null)
            {
                return Ok(new
                {
                    Message = "Event doesn't exist"
                });
            }

            if (_locationRepository.GetLocationByIdAsync(eventLocation.IdLocation) == null)
            {
                return Ok(new
                {
                    Message = "Location doesn't exist"
                });
            }

            var messageCheck = await _eventLocationRepository.CheckDateTimeEventLocationToUpdate(id, eventLocation);
            if (messageCheck != string.Empty)
            {
                return Ok(new
                {
                    Message = messageCheck.ToString()
                });
            }

            await _eventLocationRepository.UpdateEventLocationAsync(oldEventLocation, eventLocation);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/EventLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventLocation>> PostEventLocation(EventLocationDTO eventLocation)
        {
            if (_context.EventLocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EventLocations'  is null.");
            }

            var messageCheck = await _eventLocationRepository.CheckDateTimeEventLocation(eventLocation);
            if (messageCheck != string.Empty)
            {
                return Ok(new
                {
                    Message = messageCheck.ToString()
                });
            }

            if (_eventRepository.GetEventByIdAsync(eventLocation.IdEvent) == null)
            {
                return Ok(new
                {
                    Message = "Event doesn't exist"
                });
            }
            if (_locationRepository.GetLocationByIdAsync(eventLocation.IdLocation) == null)
            {
                return Ok(new
                {
                    Message = "Location doesn't exist"
                });
            }

            var result = await _eventLocationRepository.InsertEventLocationAsync(eventLocation);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/EventLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventLocation(Guid id)
        {
            if (_context.EventLocations == null)
            {
                return NotFound();
            }
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location not found"
                });
            }

            await _eventLocationRepository.DeleteEventLocationAsync(eventLocation);

            return NoContent();
        }
    }
}
