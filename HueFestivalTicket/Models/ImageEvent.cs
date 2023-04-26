using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("ImageEvent")]
    public class ImageEvent
    {
        [Key]
        public int IdImageEvent { get; set; }
        [Required]
        public string? ImageUrl { get; set; }

        [ForeignKey("IdEvent")]
        public int IdEvent { get; set; }
        public Event? Event { get; set; }
    }
}
