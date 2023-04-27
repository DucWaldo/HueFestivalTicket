using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }
        [Required]
        public string? TicketNumber { get; set; }
        [Required]
        public string? QRCode { get; set; }
        public Decimal Price { get; set; }
        public int IdEventLocation { get; set; }
        public int IdCustomer { get; set;}
        public int IdTypeTicket { get; set; }

        [ForeignKey("IdEventLocation")]
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer? Customer { get; set;}

        [ForeignKey("IdTypeTicket")]
        public TypeTicket? TypeTicket { get; set; }
    }
}
