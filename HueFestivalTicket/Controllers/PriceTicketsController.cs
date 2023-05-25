using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOrManager")]
    public class PriceTicketsController : ControllerBase
    {
        private readonly IPriceTicketRepository _priceTicketRepository;
        private readonly IEventLocationRepository _eventLocationRepository;
        private readonly ITypeTicketRepository _typeTicketRepository;

        public PriceTicketsController(IPriceTicketRepository priceTicketRepository, IEventLocationRepository eventLocationRepository, ITypeTicketRepository typeTicketRepository)
        {
            _priceTicketRepository = priceTicketRepository;
            _eventLocationRepository = eventLocationRepository;
            _typeTicketRepository = typeTicketRepository;
        }

        // GET: api/PriceTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceTicket>>> GetPriceTickets()
        {
            return await _priceTicketRepository.GetAllPriceTicketAsync();
        }

        // GET: api/PriceTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriceTicket>> GetPriceTicket(Guid id)
        {
            var priceTicket = await _priceTicketRepository.GetPriceTicketByIdAsync(id);

            if (priceTicket == null)
            {
                return Ok(new
                {
                    Message = "This Price Ticket doesn't exist"
                });
            }

            return priceTicket;
        }

        // PUT: api/PriceTickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriceTicket(Guid id, PriceTicketDTO priceTicket)
        {
            var oldPriceTicket = await _priceTicketRepository.GetPriceTicketByIdAsync(id);

            if (oldPriceTicket == null)
            {
                return Ok(new
                {
                    Message = "This Price Ticket doesn't exist"
                });
            }
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(oldPriceTicket.IdEventLocation);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "This Event Location doesn't exist"
                });
            }
            if (await _priceTicketRepository.CheckPriceTicketToUpdateAsync(priceTicket, oldPriceTicket.IdPriceTicket) == false)
            {
                return Ok(new
                {
                    Message = "Type Ticket with Event Location already exists"
                });
            }

            await _priceTicketRepository.UpdatePriceTicketAsync(oldPriceTicket, priceTicket, eventLocation.Price);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/PriceTickets
        [HttpPost]
        public async Task<ActionResult<PriceTicket>> PostPriceTicket([FromForm] PriceTicketDTO priceTicket)
        {
            var eventLocation = await _eventLocationRepository.GetEventLocationByIdAsync(priceTicket.IdEventLocation);
            if (eventLocation == null)
            {
                return Ok(new
                {
                    Message = "Event Location doesn't exist"
                });
            }
            if (eventLocation.Price == 0)
            {
                return Ok(new
                {
                    Message = "This is free event"
                });
            }
            if (priceTicket.Price < 0 || priceTicket.IdEventLocation == Guid.Empty || priceTicket.IdTypeTicket == Guid.Empty)
            {
                return Ok(new
                {
                    Message = "Please Enter All Info"
                });
            }
            if (await _typeTicketRepository.GetTypeTicketByIdAsync(priceTicket.IdTypeTicket) == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }
            if (await _priceTicketRepository.CheckPriceTicketToInsertAsync(priceTicket) == false)
            {
                return Ok(new
                {
                    Message = "Type Ticket with Event Location already exists"
                });
            }
            var result = await _priceTicketRepository.InsertPriceTicketAsync(priceTicket, eventLocation.Price);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/PriceTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriceTicket(Guid id)
        {
            var priceTicket = await _priceTicketRepository.GetPriceTicketByIdAsync(id);
            if (priceTicket == null)
            {
                return Ok(new
                {
                    Message = "This Price Ticket doesn't exist"
                });
            }
            await _priceTicketRepository.DeletePriceTicketAsync(priceTicket);
            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
