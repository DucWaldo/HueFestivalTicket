using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task DeleteTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNumberSlot(Guid idEventLocation, Guid idTypeTicket)
        {
            List<Ticket> tickets = await _dbSet.Where(t => t.IdEventLocation == idEventLocation && t.IdTypeTicket == idTypeTicket).ToListAsync();
            return tickets.Count;
        }

        public Task<Ticket?> GetTicketByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket?> GetTicketByTicketTicketNumberAsync(string ticketNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> InsertTicketAsync(TicketDTO ticket, Invoice invoice, EventLocation eventLocation, TypeTicket typeTicket, PriceTicket priceTicket)
        {
            string ticketNumber = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            var newTicket = new Ticket()
            {
                TicketNumber = ticketNumber,
                QRCode = ticketNumber + "+" + eventLocation.Event!.Name + "+" + eventLocation.Time.ToString("HH:mm") + "+" + eventLocation.DateStart.ToString("dd/MM/yyyy") + "+" + eventLocation.Location!.Title + "+" + priceTicket.Price + "+" + typeTicket.Name,
                Price = priceTicket.Price,
                IdEventLocation = ticket.IdEventLocation,
                IdInvoice = invoice.IdInvoice,
                IdTypeTicket = ticket.IdTypeTicket,
                TimeCreate = DateTime.UtcNow
            };
            await InsertAsync(newTicket);
            return newTicket;
        }

        public Task UpdateTicketAsync(Ticket oldTicket, TicketDTO newTicket)
        {
            throw new NotImplementedException();
        }
    }
}
