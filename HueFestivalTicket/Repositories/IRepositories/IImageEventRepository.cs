using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface IImageEventRepository : IRepository<ImageEvent>
    {
        public Task<List<ImageEvent>> GetAllImageEventsAsync();
        public Task<List<ImageEvent>> GetImageEventsWithIdEventAsync(Guid idEvent);
        public Task<ImageEvent?> GetImageEventByIdAsync(Guid id);
        public Task<ImageEvent?> GetImageEventByUrlAsync(string url);
        public Task<List<ImageEvent>> InsertImageEventAsync(ImageEventDTO imageEvent);
        public Task UpdateImageEventAsync(ImageEvent oldImageEvent, IFormFile file);
        public Task DeleteImageEventAsync(ImageEvent imageEvent);
    }
}
