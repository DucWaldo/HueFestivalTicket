using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Checkin")]
    public class Checkin
    {
        [Key]
        public int IdCheckin { get; set; }
        public DateTime TimeCheckin { get; set; }
        public bool Status { get; set; }

        [ForeignKey("IdAccount")]
        public int IdAccount { get; set; }
        public Account? Account { get; set; }

        [ForeignKey("IdTicket")]
        public int IdTicket { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
