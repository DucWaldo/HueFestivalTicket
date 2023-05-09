using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IPriceTicketRepository : IRepository<PriceTicket>
    {
        public Task<List<PriceTicket>> GetAllPriceTicketAsync();
        public Task<PriceTicket?> GetPriceTicketByIdAsync(Guid id);
        public Task<PriceTicket> InsertPriceTicketAsync(PriceTicketDTO priceTicket, decimal price);
        public Task UpdatePriceTicketAsync(PriceTicket oldPriceTicket, PriceTicketDTO newPriceTicket, decimal price);
        public Task DeletePriceTicketAsync(PriceTicket priceTicket);
        public Task<bool> CheckPriceTicketToInsertAsync(PriceTicketDTO priceTicket);
        public Task<bool> CheckPriceTicketToUpdateAsync(PriceTicketDTO priceTicket, Guid id);
    }
}
