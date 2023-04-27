using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int IdAccount { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public DateTime TimeJoined { get; set; }
    }
}
