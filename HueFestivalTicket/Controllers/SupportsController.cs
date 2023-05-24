using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISupportRepository _supportRepository;

        public SupportsController(ApplicationDbContext context, ISupportRepository supportRepository)
        {
            _context = context;
            _supportRepository = supportRepository;
        }

        // GET: api/Supports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Support>>> GetSupports()
        {
            return await _supportRepository.GetAllSupportAsync();
        }

        // GET: api/Supports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Support>> GetSupport(Guid id)
        {
            var support = await _supportRepository.GetSupportByIdAsync(id);

            if (support == null)
            {
                return Ok(new
                {
                    Message = "This Support not found"
                });
            }

            return support;
        }

        // PUT: api/Supports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> PutSupport(Guid id, SupportDTO newSupport)
        {
            var oldSupport = await _supportRepository.GetSupportByIdAsync(id);
            if (oldSupport == null)
            {
                return Ok(new
                {
                    Message = "This Support not found"
                });
            }

            var checkTitle = await _supportRepository.GetSupportByTitleAsync(newSupport.Title ?? "");
            if (checkTitle != null)
            {
                return Ok(new
                {
                    Message = $"Title {newSupport.Title} already exists"
                });
            }

            await _supportRepository.UpdateSupportAsync(oldSupport, newSupport);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Supports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult<Support>> PostSupport(SupportDTO support)
        {
            var checkTitle = await _supportRepository.GetSupportByTitleAsync(support.Title ?? "");

            if (checkTitle != null)
            {
                return Ok(new
                {
                    Message = $"Title {support.Title} already exists"
                });
            }

            var result = await _supportRepository.InsertSupportAsync(support, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? ""));

            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/Supports/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> DeleteSupport(Guid id)
        {
            var support = await _supportRepository.GetSupportByIdAsync(id);
            if (support == null)
            {
                return Ok(new
                {
                    Message = "This Support not found"
                });
            }

            await _supportRepository.DeleteSupportAsync(support);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
