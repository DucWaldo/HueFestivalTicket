using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ManagerPolicy")]
    public class TypeTicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITypeTicketRepository _typeTicketRepository;

        public TypeTicketsController(ApplicationDbContext context, ITypeTicketRepository typeTicketRepository)
        {
            _context = context;
            _typeTicketRepository = typeTicketRepository;
        }

        // GET: api/TypeTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeTicket>>> GetTypeTickets()
        {
            return await _typeTicketRepository.GetAllTypeTicketAsync();
        }

        // GET: api/TypeTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeTicket>> GetTypeTicket(Guid id)
        {
            var typeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(id);

            if (typeTicket == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }

            return typeTicket;
        }

        // PUT: api/TypeTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeTicket(Guid id, TypeTicketDTO typeTicket)
        {
            var oldTypeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(id);
            if (oldTypeTicket == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }
            if (await _typeTicketRepository.CheckNameTypeTicketAsync(oldTypeTicket.IdTypeTicket, typeTicket.Name!) == false)
            {
                return Ok(new
                {
                    Message = "This Name already exist"
                });
            }
            await _typeTicketRepository.UpdateTypeTicketAsync(oldTypeTicket, typeTicket);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/TypeTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeTicket>> PostTypeTicket(TypeTicketDTO typeTicket)
        {
            if (typeTicket.Name == null || await _typeTicketRepository.GetTypeTicketByNameAsync(typeTicket.Name) != null)
            {
                return Ok(new
                {
                    Message = "Type Ticket Name is empty or already exist"
                });
            }
            var result = await _typeTicketRepository.InsertTypeTicketAsync(typeTicket);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/TypeTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeTicket(Guid id)
        {
            var typeTicket = await _typeTicketRepository.GetTypeTicketByIdAsync(id);
            if (typeTicket == null)
            {
                return Ok(new
                {
                    Message = "This Type Ticket doesn't exist"
                });
            }

            await _typeTicketRepository.DeleteTypeTicketAsync(typeTicket);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
