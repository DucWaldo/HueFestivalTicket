namespace HueFestivalTicket.Data
{
    public class InvoiceDTO
    {
        public Guid IdInvoice { get; set; }
        public decimal Total { get; set; }
        public Guid IdCustomer { get; set; }
    }
}
