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
                _dbSet.Remove(imageEvent);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<ImageEvent>> GetAllImageEventsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ImageEvent> GetImageEventByIdAsync(Guid id)
        {
            var imageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.IdImageEvent == id);
            return imageEvent ?? new ImageEvent();
        }

        public Task GetImageEventByUrlAsync(string url)
        {
            throw new NotImplementedException();
        }

        public async Task InsertImageEventAsync(ImageEvent imageEvent)
        {
            await _dbSet.AddAsync(imageEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateImageEventAsync(Guid id, string url)
        {
            var existingImageEvent = await _dbSet.FirstOrDefaultAsync(ie => ie.IdImageEvent == id);

            if (existingImageEvent != null)
            {
                existingImageEvent.ImageUrl = url;
                _dbSet.Update(existingImageEvent);
                await _context.SaveChangesAsync();
            }

        }
    }
}
