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
        public Decimal Price { get; set; }
        public Guid IdEventLocation { get; set; }
        public Guid IdCustomer { get; set;}
        public Guid IdTypeTicket { get; set; }

        [ForeignKey("IdEventLocation")]
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer? Customer { get; set;}

        [ForeignKey("IdTypeTicket")]
        public TypeTicket? TypeTicket { get; set; }
    }
}
