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
        public int IdAccount { get; set; }
        public int IdTicket { get; set; }

        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }

        [ForeignKey("IdTicket")]
        public Ticket? Ticket { get; set; }
    }
}
