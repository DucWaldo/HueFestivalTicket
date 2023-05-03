
using HueFestivalTicket.Models;

namespace HueFestivalTicket.Data
{
    public class UserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Organization { get; set; }

        public static UserDTO FromUser(User user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Organization = user.Organization
            };
        }
    }
}
