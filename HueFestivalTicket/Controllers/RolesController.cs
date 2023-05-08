using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoleRepository _roleRepository;

        public RolesController(ApplicationDbContext context, IMapper mapper, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }

            return await _roleRepository.GetAllRolesAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{name}")]
        public async Task<ActionResult<RoleDTO>> GetRole(string name)
        {


            if (_context.Roles == null)
            {
                return NotFound();
            }
            var role = await _roleRepository.GetRoleByNameAsync(name);
            if (role == null)
            {
                return Ok(new
                {
                    Message = "Role doesn't exist"
                });
            }
            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{oldName}")]
        public async Task<IActionResult> PutRole(string oldName, RoleDTO newName)
        {
            var roles = await _roleRepository.GetRoleByNameAsync(oldName ?? "");
            if (roles == null)
            {
                return Ok(new
                {
                    Message = "This Role not found"
                });
            }

            var checkNewName = await _roleRepository.GetRoleByNameAsync(newName.Name ?? "");
            if (checkNewName != null)
            {
                return Ok(new
                {
                    Message = "Role name already exists"
                });
            }

            await _roleRepository.UpdateRoleAsync(oldName ?? "", newName);
            return Ok(new
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
            var roleName = await _roleRepository.GetRoleByNameAsync(role.Name ?? "");
            if (roleName != null)
            {
                return Ok(new
                {
                    Message = "Role already exists"
                });
            }

            await _roleRepository.InsertRoleAsync(role);
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
            var role = await _roleRepository.GetRoleByNameAsync(name ?? "");
            if (role == null)
            {
                return NotFound();
            }

            await _roleRepository.DeleteRoleAsync(name ?? "");

            return Ok(new
            {
                Message = $"Delete {name} success"
            });
        }
    }
}
