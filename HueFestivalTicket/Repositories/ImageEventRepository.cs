using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class ImageEventRepository : RepositoryBase<ImageEvent>, IImageEventRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ImageEventRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteImageEventAsync(Guid id)
        {
            var imageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.IdImageEvent == id);
            if (imageEvent != null)
            {
                await DeleteAsync(imageEvent);
            }
        }

        public async Task<List<ImageEvent>> GetAllImageEventsAsync()
        {
            var imageEvents = await GetAllAsync();
            return imageEvents;
        }

        public async Task<ImageEvent?> GetImageEventByIdAsync(Guid id)
        {
            var imageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.IdImageEvent == id);
            return imageEvent;
        }

        public async Task<ImageEvent?> GetImageEventByUrlAsync(string url)
        {
            var imageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.ImageUrl == url);
            return imageEvent;
        }

        public async Task InsertImageEventAsync(ImageEvent imageEvent)
        {
            await InsertAsync(imageEvent);
        }

        public async Task UpdateImageEventAsync(Guid id, string url)
        {
            var existingImageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.IdImageEvent == id);

            if (existingImageEvent != null)
            {
                existingImageEvent.ImageUrl = url;
                await UpdateAsync(existingImageEvent);
            }

        }
    }
}
