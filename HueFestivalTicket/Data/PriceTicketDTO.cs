namespace HueFestivalTicket.Data
{
    public class PriceTicketDTO
    {
        public decimal Price { get; set; }
        public Guid IdEventLocation { get; set; }
        public Guid IdTypeTicket { get; set; }
    }
}
