﻿namespace HueFestivalTicket.Data
{
    public class TicketDTO
    {
        public Guid IdEventLocation { get; set; }
        public Guid IdTypeTicket { get; set; }
        public string? IdCardCustomer { get; set; }
        public int Number { get; set; }
    }
}
