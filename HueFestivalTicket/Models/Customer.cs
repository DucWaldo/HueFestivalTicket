using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public Guid IdCustomer { get; set; }
        [Required]
        [MaxLength(255)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Ivalid Email")]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "Ivalid Phone")]
        public string? PhoneNumber { get; set; }
        public string? IdCard { get; set; }
    }
}
