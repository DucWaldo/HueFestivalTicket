using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventLocation>>> GetEventLocations()
        {
          if (_context.EventLocations == null)
          {
              return NotFound();
          }
            return await _context.EventLocations.ToListAsync();
        }

        // GET: api/EventLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventLocation>> GetEventLocation(Guid id)
        {
          if (_context.EventLocations == null)
          {
              return NotFound();
          }
            var eventLocation = await _context.EventLocations.FindAsync(id);

            if (eventLocation == null)
            {
                return NotFound();
            }

            return eventLocation;
        }

        // PUT: api/EventLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventLocation(Guid id, EventLocation eventLocation)
        {
            if (id != eventLocation.IdEventLocation)
            {
                return BadRequest();
            }

            _context.Entry(eventLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventLocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EventLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventLocation>> PostEventLocation(EventLocation eventLocation)
        {
          if (_context.EventLocations == null)
          {
              return Problem("Entity set 'ApplicationDbContext.EventLocations'  is null.");
          }
            _context.EventLocations.Add(eventLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventLocation", new { id = eventLocation.IdEventLocation }, eventLocation);
        }

        // DELETE: api/EventLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventLocation(Guid id)
        {
            if (_context.EventLocations == null)
            {
                return NotFound();
            }
            var eventLocation = await _context.EventLocations.FindAsync(id);
            if (eventLocation == null)
            {
                return NotFound();
            }

            _context.EventLocations.Remove(eventLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventLocationExists(Guid id)
        {
            return (_context.EventLocations?.Any(e => e.IdEventLocation == id)).GetValueOrDefault();
        }
    }
}
