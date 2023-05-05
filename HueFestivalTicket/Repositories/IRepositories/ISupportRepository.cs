using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface ISupportRepository : IRepository<Support>
    {
        public Task<List<Support>> GetAllSupportAsync();
        public Task<Support?> GetSupportByIdAsync(Guid id);
        public Task<Support?> GetSupportByTitleAsync(string title);
        public Task<Support> InsertSupportAsync(SupportDTO support, Guid idAccount);
        public Task UpdateSupportAsync(Support oldSupport, SupportDTO newSupport);
        public Task DeleteSupportAsync(Support support);
    }
}
