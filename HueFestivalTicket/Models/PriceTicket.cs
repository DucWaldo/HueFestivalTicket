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

        [ForeignKey("IdEventLocation")]
        public int IdEventLocation { get; set; }
        public EventLocation? EventLocation { get; set; }

        [ForeignKey("IdTypeTicket")]
        public int IdTypeTicket { get; set; }
        public TypeTicket? TypeTicket { get; set; }
    }
}
