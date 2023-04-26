using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int IdLocation { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("IdTypeLocation")]
        public int IdTypeLocation { get; set; }
        public TypeLocation? TypeLocation { get; set; }
    }
}
