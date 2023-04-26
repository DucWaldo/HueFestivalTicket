using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("AccountRole")]
    public class AccountRole
    {
        [Key]
        public int IdAccountRole { get; set; }

        [ForeignKey("IdAccount")]
        public int IdAccount { get; set; }
        public Account? Account { get; set; }
        
        [ForeignKey("IdRole")]
        public int IdRole { get; set; }
        public Role? Role { get; set; }
    }
}
