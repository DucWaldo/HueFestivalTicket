using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public int IdEvent { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        public string? Content { get; set; }
        [DefaultValue(false)]
        public bool StatusTicket { get; set; }
        [Required]
        public int TypeEvent { get; set; }
    }
}
