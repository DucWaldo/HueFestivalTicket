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

        [ForeignKey("IdEventLocation")]
        public int IdEventLocation { get; set; }
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdCustomer")]
        public int IdCustomer { get; set;}
        public Customer? Customer { get; set;}

        [ForeignKey("IdTypeTicket")]
        public int IdTypeTicket { get; set; }
        public TypeTicket? TypeTicket { get; set; }
    }
}
