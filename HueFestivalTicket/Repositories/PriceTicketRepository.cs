using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class PriceTicketRepository : RepositoryBase<PriceTicket>, IPriceTicketRepository
    {
        public PriceTicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckPriceTicketToInsertAsync(PriceTicketDTO priceTicket)
        {
            var check = await _dbSet.FirstOrDefaultAsync(pt => pt.IdEventLocation == priceTicket.IdEventLocation && pt.IdTypeTicket == priceTicket.IdTypeTicket);
            if (check != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckPriceTicketToUpdateAsync(PriceTicketDTO priceTicket, Guid id)
        {
            var check = await _dbSet.FirstOrDefaultAsync(pt => pt.IdEventLocation == priceTicket.IdEventLocation && pt.IdTypeTicket == priceTicket.IdTypeTicket && pt.IdPriceTicket != id);
            if (check != null)
            {
                return false;
            }
            return true;
        }

        public async Task DeletePriceTicketAsync(PriceTicket priceTicket)
        {
            await DeleteAsync(priceTicket);
        }

        public async Task<List<PriceTicket>> GetAllPriceTicketAsync()
        {
            return await GetAllWithIncludesAsync(pt => pt.EventLocation!.Event!, pt => pt.EventLocation!.Location!.TypeLocation!, pt => pt.TypeTicket!);
        }

        public async Task<PriceTicket?> GetPriceTicketByIdAsync(Guid id)
        {
            var priceTicket = await _dbSet.FirstOrDefaultAsync(pt => pt.IdPriceTicket == id);
            return priceTicket;
        }

        public async Task<PriceTicket?> GetPriceTicketByIdEventLocationAndIdTypeTicketAsync(Guid idEventLocation, Guid idTypeTicket)
        {
            var priceTicket = await _dbSet.FirstOrDefaultAsync(pt => pt.IdEventLocation == idEventLocation && pt.IdTypeTicket == idTypeTicket);
            return priceTicket;
        }

        public async Task<PriceTicket> InsertPriceTicketAsync(PriceTicketDTO priceTicket, decimal price)
        {
            var newPriceTicket = new PriceTicket()
            {
                Price = priceTicket.Price + price,
                IdEventLocation = priceTicket.IdEventLocation,
                IdTypeTicket = priceTicket.IdTypeTicket
            };
            await InsertAsync(newPriceTicket);
            return newPriceTicket;
        }

        public async Task UpdatePriceTicketAsync(PriceTicket oldPriceTicket, PriceTicketDTO newPriceTicket, decimal price)
        {
            oldPriceTicket.Price = newPriceTicket.Price + price;
            oldPriceTicket.IdEventLocation = newPriceTicket.IdEventLocation;
            oldPriceTicket.IdTypeTicket = newPriceTicket.IdTypeTicket;

            await UpdateAsync(oldPriceTicket);
        }
    }
}
