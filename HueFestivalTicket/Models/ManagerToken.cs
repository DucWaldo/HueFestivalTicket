using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("ManagerToken")]
    public class ManagerToken
    {
        [Key]
        public Guid IdRefreshToken { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public Guid IdAccount { get; set; }
        
        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }
    }
}
