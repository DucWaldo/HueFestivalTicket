using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("TypeLocation")]
    public class TypeLocation
    {
        [Key]
        public Guid IdTypeLocation { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
