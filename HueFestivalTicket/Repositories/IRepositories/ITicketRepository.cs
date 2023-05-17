using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<List<Ticket>> GetAllTicketsAsync();
        public Task<object> GetTicketPagingAsync(int pageNumber, int pageSize);
        public Task<Ticket?> GetTicketByIdAsync(Guid id);
        public Task<Ticket?> GetTicketByTicketNumberAsync(string ticketNumber);
        public Task<int> GetNumberSlot(Guid IdEventLocation, Guid idTypeTicket);
        public Task<Ticket> InsertTicketAsync(TicketDTO ticket, Invoice invoice, EventLocation eventLocation, TypeTicket typeTicket, PriceTicket priceTicket);
        public Task UpdateTicketAsync(Ticket oldTicket, TicketDTO newTicket);
        public Task DeleteTicketAsync(Ticket ticket);
    }
}
