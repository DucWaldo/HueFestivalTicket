using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<List<Ticket>> GetAllTicketsAsync();
        public Task<Ticket?> GetTicketByIdAsync(Guid id);
        public Task<Ticket?> GetTicketByTicketTicketNumberAsync(string ticketNumber);
        public Task<int> GetNumberSlot(Guid IdEventLocation);
        public Task<Ticket> InsertTicketAsync(TicketDTO ticket, Invoice invoice, EventLocation eventLocation, TypeTicket typeTicket, PriceTicket priceTicket);
        public Task UpdateTicketAsync(Ticket oldTicket, TicketDTO newTicket);
        public Task DeleteTicketAsync(Ticket ticket);
    }
}
