using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("ImageEvent")]
    public class ImageEvent
    {
        [Key]
        public Guid IdImageEvent { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public Guid IdEvent { get; set; }

        [ForeignKey("IdEvent")]
        public Event? Event { get; set; }
    }
}
