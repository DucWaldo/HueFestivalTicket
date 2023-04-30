using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using Microsoft.AspNetCore.Authorization;
using HueFestivalTicket.Data;
using AutoMapper;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace HueFestivalTicket.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<List<Role>, List<RoleDTO>>(await _context.Roles.ToListAsync());

            return result;
        }

        // GET: api/Roles/5
        [HttpGet("{name}")]
        public async Task<ActionResult<RoleDTO>> GetRole(string name)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            
            if (role == null)
            {
                return Ok(new
                {
                    Message = "Role doesn't exist"
                });
            }
            var result = _mapper.Map<Role, RoleDTO>(role);
            return result;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{oldName}")]
        public async Task<IActionResult> PutRole(string oldName, RoleDTO newName)
        {
            var roles = await _context.Roles.FirstOrDefaultAsync(r => r.Name == oldName);
            if (roles == null)
            {
                return Ok(new
                {
                    Message = "Role not found"
                });
            }

            var check = await _context.Roles.FirstOrDefaultAsync(r => r.Name == newName.Name);
            if (check != null)
            {
                return Ok(new
                {
                    Message = "Role name already exists"
                });
            }

            roles.Name = newName.Name;

            await _context.SaveChangesAsync();
            return Ok( new
            {
                Message = $"Update {oldName} to {newName.Name} Success"
            });
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDTO role)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
          }
            var roleName = await _context.Roles.FirstOrDefaultAsync(x => x.Name == role.Name);
            if (roleName != null)
            {
                return Ok(new
                {
                    Message = "Role already exists"
                });
            }

            var roles = new Role
            {
                Name = role.Name
            };

            await _context.Roles.AddAsync(roles);
            await _context.SaveChangesAsync();

            return Ok(new 
            { 
                Message = "Create Success", 
                role 
            });
        }

        // DELETE: api/Roles/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteRole(string name)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            //var result = await _context.Roles.ToListAsync();
            var result = _mapper.Map<List<Role>, List<RoleDTO>>(await _context.Roles.ToListAsync());
            return Ok(new
            {
                Message = $"Delete {name} success",
                result
            });
        }

        private bool RoleExists(Guid id)
        {
            return (_context.Roles?.Any(e => e.IdRole == id)).GetValueOrDefault();
        }
    }
}
