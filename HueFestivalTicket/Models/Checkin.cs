using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Checkin")]
    public class Checkin
    {
        [Key]
        public Guid IdCheckin { get; set; }
        public string? QRCodeContent { get; set; }
        public DateTime TimeCheckin { get; set; }
        public bool Status { get; set; }
        public Guid IdAccount { get; set; }

        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }
    }
}
