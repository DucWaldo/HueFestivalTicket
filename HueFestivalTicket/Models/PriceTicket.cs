using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("PriceTicket")]
    public class PriceTicket
    {
        [Key]
        public int IdPriceTicket { get; set; }
        public Decimal Price { get; set; }
        public int IdEventLocation { get; set; }
        public int IdTypeTicket { get; set; }

        [ForeignKey("IdEventLocation")]
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdTypeTicket")]
        public TypeTicket? TypeTicket { get; set; }
    }
}
