using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Support")]
    public class Support
    {
        [Key]
        public int IdSuport { get; set; }
        [Required]
        [StringLength(255)]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
