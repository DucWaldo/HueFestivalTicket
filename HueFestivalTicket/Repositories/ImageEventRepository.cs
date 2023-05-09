using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
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
        private readonly IWebHostEnvironment _environment;

        public ImageEventRepository(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task DeleteImageEventAsync(ImageEvent imageEvent)
        {
            DeleteFile(imageEvent.ImageUrl);
            await DeleteAsync(imageEvent);
        }

        public async Task<List<ImageEvent>> GetAllImageEventsAsync()
        {
            return await GetAllWithIncludesAsync(ie => ie.Event!);
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

        public async Task<List<ImageEvent>> InsertImageEventAsync(ImageEventDTO imageEvent)
        {
            List<ImageEvent> imageEvents = new List<ImageEvent>();
            foreach (var file in imageEvent.ImageUrl!)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                InsertFile(file, imageName);
                var newImageEvent = new ImageEvent
                {
                    ImageUrl = "/images/" + imageName,
                    IdEvent = imageEvent.IdEvent
                };
                await InsertAsync(newImageEvent);
                imageEvents.Add(newImageEvent);
            }
            return imageEvents;
        }

        public async Task UpdateImageEventAsync(ImageEvent oldImageEvent, IFormFile file)
        {
            DeleteFile(oldImageEvent.ImageUrl);

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            InsertFile(file, imageName);

            var url = "/images/" + imageName;
            oldImageEvent.ImageUrl = url;

            await UpdateAsync(oldImageEvent);
        }

        private void DeleteFile(string? imageUrl)
        {
            var imagePath = _environment.WebRootPath + imageUrl;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        private async void InsertFile(IFormFile file, string imageName)
        {
            var newImagePath = _environment.WebRootPath + "\\images\\";
            if (!Directory.Exists(newImagePath))
            {
                Directory.CreateDirectory(newImagePath);
            }
            using (FileStream stream = System.IO.File.Create(newImagePath + imageName))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }
        }
    }
}
