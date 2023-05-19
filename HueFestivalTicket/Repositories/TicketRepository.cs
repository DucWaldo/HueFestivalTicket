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

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await GetAllWithIncludesAsync(t => t.EventLocation!.Event!);
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

        public async Task<Ticket?> GetTicketByTicketNumberAsync(string ticketNumber)
        {
            var ticket = await _dbSet.Include(t => t.EventLocation!.Event)
                .Include(t => t.EventLocation!.Location!.TypeLocation)
                .Include(t => t.TypeTicket)
                .Include(t => t.Invoice!.Customer)
                .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
            return ticket;
        }

        public async Task<object> GetTicketPagingAsync(int pageNumber, int pageSize)
        {
            List<Ticket> data = await GetPage(pageNumber, pageSize, t => t.TicketNumber!, t => t.EventLocation!.Event!, t => t.EventLocation!.Location!.TypeLocation!, t => t.Invoice!.Customer!, t => t.TypeTicket!);
            return ReturnGetPage(data, pageNumber, pageSize);
        }

        public async Task<Ticket> InsertTicketAsync(TicketDTO ticket, Invoice invoice, EventLocation eventLocation, TypeTicket typeTicket, PriceTicket priceTicket)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string ticketNumber = DateTime.UtcNow.ToString("yyyyMMddHHmmssff") + randomNumber.ToString();
            var newTicket = new Ticket()
            {
                TicketNumber = ticketNumber,
                QRCode = ticketNumber + "|" + eventLocation.Event!.Name + "|" + eventLocation.Time.ToString("HH:mm") + "|" + eventLocation.DateStart.ToString("dd/MM/yyyy") + "|" + eventLocation.Location!.Title + "|" + priceTicket.Price + "|" + typeTicket.Name,
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
