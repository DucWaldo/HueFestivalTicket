using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("PriceTicket")]
    public class PriceTicket
    {
        [Key]
        public Guid IdPriceTicket { get; set; }
        public Decimal Price { get; set; }
        public Guid IdEventLocation { get; set; }
        public Guid IdTypeTicket { get; set; }

        [ForeignKey("IdEventLocation")]
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdTypeTicket")]
        public TypeTicket? TypeTicket { get; set; }
    }
}
