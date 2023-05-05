namespace HueFestivalTicket.Data
{
    public class LocationDTO
    {
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public string? Address { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public Guid IdTypeLocation { get; set; }
    }
}
