using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HueFestivalTicket.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public Guid IdInvoice { get; set; }
        public DateTime TimeCreate { get; set; }
        public decimal Total { get; set; }
        public Guid IdCustomer { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer? Customer { get; set; }
    }
}
