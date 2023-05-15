using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("EventLocation")]
    public class EventLocation
    {
        [Key]
        public Guid IdEventLocation { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public Guid IdEvent { get; set; }
        public Guid IdLocation { get; set; }

        [ForeignKey("IdEvent")]
        public Event? Event { get; set; }

        [ForeignKey("IdLocation")]
        public Location? Location { get; set; }
    }
}
