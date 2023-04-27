using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        [MaxLength(255)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string? LastName { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Ivalid Email")]
        public string? Email { get; set; }
        public string? Organization { get; set; }
        public int IdAccount { get; set; }

        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }
    }
}
