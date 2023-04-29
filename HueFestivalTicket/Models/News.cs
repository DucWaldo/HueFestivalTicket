using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public Guid IdNews { get; set; }
        [Required]
        [StringLength(255)]
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime TimeCreate { get; set; }
        public string? ImageUrl { get; set; }
        public Guid IdAccount { get; set; }

        [ForeignKey("IdAccount")]
        public Account? Account { get; set; }
    }
}
