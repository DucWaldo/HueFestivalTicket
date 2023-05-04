using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IImageEventRepository : IRepository<ImageEvent>
    {
        public Task<List<ImageEvent>> GetAllImageEventsAsync();
        public Task<ImageEvent?> GetImageEventByIdAsync(Guid id);
        public Task<ImageEvent?> GetImageEventByUrlAsync(string url);
        public Task InsertImageEventAsync(ImageEvent imageEvent);
        public Task UpdateImageEventAsync(Guid id, string url);
        public Task DeleteImageEventAsync(Guid id);
    }
}
