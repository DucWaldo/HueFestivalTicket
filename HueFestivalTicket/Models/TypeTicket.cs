using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("TypeTicket")]
    public class TypeTicket
    {
        [Key]
        public int IdTypeTicket { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
