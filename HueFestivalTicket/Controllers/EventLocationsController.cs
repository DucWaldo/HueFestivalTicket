﻿using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventLocationsController : ControllerBase
    {
        private readonly IEventLocationRepository _eventLocationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPriceTicketRepository _priceTicketRepository;

        public EventLocationsController(IEventLocationRepository eventLocationRepository,
            ILocationRepository locationRepository,
            IEventRepository eventRepository,
            IPriceTicketRepository priceTicketRepository)
        {
            _eventLocationRepository = eventLocationRepository;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
            _priceTicketRepository = priceTicketRepository;
        }

        // GET: api/EventLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventLocation>>> GetEventLocations()
        {
            return await _eventLocationRepository.GetAllEventLocationsAsync();
        }

        // GET: api/EventLocations/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<EventLocation>>> GetEventLocationPaging(int pageNumber, int pageSize)
        {
            var result = await _eventLocationRepository.GetEventLocationPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/EventLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventLocation>> GetEventLocation(Guid id)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);

            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }

            var priceTicket = await _priceTicketRepository.GetPriceTicketByIdEventLocationAsync(eventLocation.IdEventLocation);

            return Ok(new
            {
                eventLocation,
                priceTicket
            });
        }

        // PUT: api/EventLocations/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
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

            var eventCheck = await _eventRepository.GetEventByIdAsync(eventLocation.IdEvent);
            if (eventCheck == null)
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

            if (eventCheck.StatusTicket == false && eventLocation.Price > 0)
            {
                return Ok(new
                {
                    Message = "This Event doesn't sell tickets, please enter the number slot = 0 and price = 0"
                });
            }
            if (eventCheck.StatusTicket == true && eventLocation.Price <= 0)
            {
                return Ok(new
                {
                    Message = "Please enter number slot or price"
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

        // PUT: api/EventLocations/5
        [HttpPut("UpdateStatus")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> UpdateStatusEventLocation(Guid id, bool status)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "Event Location not found"
                });
            }
            if (status == eventLocation.Status)
            {
                return Ok(new
                {
                    Message = "Nothing Changes"
                });
            }
            if (status == true && eventLocation.DateEnd < DateTime.Today)
            {
                return Ok(new
                {
                    Message = "Event Location has ended, can't be reactivated"
                });
            }
            await _eventLocationRepository.UpdateStatusEventLocationAsync(eventLocation, status);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/EventLocations
        [HttpPost]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult<EventLocation>> PostEventLocation(EventLocationDTO eventLocation)
        {
            var messageCheck = await _eventLocationRepository.CheckDateTimeEventLocation(eventLocation);
            if (messageCheck != string.Empty)
            {
                return Ok(new
                {
                    Message = messageCheck.ToString()
                });
            }

            var eventCheck = await _eventRepository.GetEventByIdAsync(eventLocation.IdEvent);
            if (eventCheck == null)
            {
                return Ok(new
                {
                    Message = "Event doesn't exist"
                });
            }
            if (await _locationRepository.GetLocationByIdAsync(eventLocation.IdLocation) == null)
            {
                return Ok(new
                {
                    Message = "Location doesn't exist"
                });
            }
            if (eventCheck.StatusTicket == false && eventLocation.Price > 0)
            {
                return Ok(new
                {
                    Message = "This Event doesn't sell tickets, please enter the price = 0"
                });
            }
            if (eventCheck.StatusTicket == true && eventLocation.Price <= 0)
            {
                return Ok(new
                {
                    Message = "Please enter Price"
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
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> DeleteEventLocation(Guid id)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(id);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location not found"
                });
            }

            await _eventLocationRepository.DeleteEventLocationAsync(eventLocation);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
