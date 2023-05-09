using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ITypeTicketRepository : IRepository<TypeTicket>
    {
        public Task<List<TypeTicket>> GetAllTypeTicketAsync();
        public Task<TypeTicket?> GetTypeTicketByIdAsync(Guid id);
        public Task<TypeTicket?> GetTypeTicketByNameAsync(string name);
        public Task<TypeTicket> InsertTypeTicketAsync(TypeTicketDTO typeTicket);
        public Task UpdateTypeTicketAsync(TypeTicket oldTypeTicket, TypeTicketDTO newTypeTicket);
        public Task DeleteTypeTicketAsync(TypeTicket typeTicket);
        public Task<bool> CheckNameTypeTicketAsync(Guid id, string name);
    }
}
