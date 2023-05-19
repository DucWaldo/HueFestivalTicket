
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("ManagerVerify")]
    public class ManagerVerify
    {
        [Key]
        public Guid IdVerifyCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Code { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public bool Status { get; set; }
    }
}
