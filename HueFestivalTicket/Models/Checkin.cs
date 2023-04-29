using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Checkin")]
    public class Checkin
    {
        [Key]
        public Guid IdCheckin { get; set; }
        public DateTime TimeCheckin { get; set; }
        public bool Status { get; set; }
        public Guid IdAccount { get; set; }
        public Guid IdTicket { get; set; }

        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }

        [ForeignKey("IdTicket")]
        public Ticket? Ticket { get; set; }
    }
}
