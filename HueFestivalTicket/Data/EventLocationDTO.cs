namespace HueFestivalTicket.Data
{
    public class EventLocationDTO
    {
        public string? DateStart { get; set; }
        public string? DateEnd { get; set; }
        public string? Time { get; set; }
        public int NumberSlot { get; set; }
        public decimal Price { get; set; }
        public Guid IdEvent { get; set; }
        public Guid IdLocation { get; set; }
    }
}
