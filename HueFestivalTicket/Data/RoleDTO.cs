using HueFestivalTicket.Models;

namespace HueFestivalTicket.Data
{
    public class RoleDTO
    {
        public string? Name { get; set; }

        public static RoleDTO FromRole(Role role)
        {
            return new RoleDTO
            {
                Name = role.Name
            };
        }
    }
}
