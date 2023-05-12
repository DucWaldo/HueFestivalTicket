using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public Guid IdTicket { get; set; }
        [Required]
        public string? TicketNumber { get; set; }
        [Required]
        public string? QRCode { get; set; }
        public DateTime TimeCreate { get; set; }
        public decimal Price { get; set; }
        public Guid IdEventLocation { get; set; }
        public Guid IdTypeTicket { get; set; }
        public Guid IdInvoice { get; set; }

        [ForeignKey("IdEventLocation")]
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdTypeTicket")]
        public TypeTicket? TypeTicket { get; set; }

        [ForeignKey("IdInvoice")]
        public Invoice? Invoice { get; set; }
    }
}
