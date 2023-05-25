using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{name}")]
        public async Task<ActionResult<RoleDTO>> GetRole(string name)
        {
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
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleDTO role)
        {
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
