﻿using HueFestivalTicket.Contexts;
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
            if (_context.Supports == null)
            {
                return NotFound();
            }
            return await _supportRepository.GetAllSupportAsync();
        }

        // GET: api/Supports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Support>> GetSupport(Guid id)
        {
            if (_context.Supports == null)
            {
                return NotFound();
            }
            var support = await _supportRepository.GetSupportByIdAsync(id);

            if (support == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} doesn't exist"
                });
            }

            return support;
        }

        // PUT: api/Supports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupport(Guid id, SupportDTO support)
        {
            var oldSupport = await _supportRepository.GetSupportByIdAsync(id);
            if (oldSupport == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} doesn't exist"
                });
            }
            await _supportRepository.UpdateSupportAsync(oldSupport, support);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Supports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Support>> PostSupport(SupportDTO support)
        {
            if (_context.Supports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Supports'  is null.");
            }

            var checkTitle = await _supportRepository.GetSupportByTitleAsync(support.Title ?? "");

            if (checkTitle != null)
            {
                return Ok(new
                {
                    Message = $"Title {support.Title} already exists"
                });
            }

            var newSupport = new Support
            {
                Title = support.Title,
                Content = support.Content,
                IdAccount = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "")
            };

            await _supportRepository.InsertSupportAsync(newSupport);

            return Ok(new
            {
                Message = "Insert Success",
                support
            });
        }

        // DELETE: api/Supports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupport(Guid id)
        {
            if (_context.Supports == null)
            {
                return NotFound();
            }
            var support = await _supportRepository.GetSupportByIdAsync(id);
            if (support == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} doesn't exist"
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