using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
