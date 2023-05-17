using AutoMapper;
using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public LocationRepository(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment) : base(context)
        {
            _mapper = mapper;
            _environment = environment;
        }

        public bool CheckImage(IFormFile? file)
        {
            if (file == null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteLocationAsync(Location location)
        {
            DeleteFile(location.ImageUrl);
            await DeleteAsync(location);
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await GetAllWithIncludesAsync(l => l.TypeLocation!);
        }

        public async Task<Location?> GetLocationByIdAsync(Guid id)
        {
            var location = await _dbSet.FirstOrDefaultAsync(l => l.IdLocation == id);
            return location;
        }

        public async Task<object> GetLocationPagingAsync(int pageNumber, int pageSize)
        {
            List<Location> data = await GetPage(pageNumber, pageSize, l => l.Title!, l => l.TypeLocation!);
            return ReturnGetPage(data, pageNumber, pageSize);
        }

        public async Task<Location> InsertLocationAsync(LocationDTO location)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(location.ImageUrl?.FileName);
            InsertFile(location.ImageUrl, imageName);

            var newLocation = new Location
            {
                Title = location.Title,
                Decription = location.Decription,
                Address = location.Address,
                ImageUrl = "/images/" + imageName,
                IdTypeLocation = location.IdTypeLocation
            };
            await InsertAsync(newLocation);
            return newLocation;
        }

        public async Task UpdateLocationAsync(Location oldLocation, LocationDTO newLocation)
        {
            DeleteFile(oldLocation.ImageUrl);
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(newLocation.ImageUrl?.FileName);
            InsertFile(newLocation.ImageUrl, imageName);

            oldLocation.Title = newLocation.Title;
            oldLocation.Decription = newLocation.Decription;
            oldLocation.Address = newLocation.Address;
            oldLocation.ImageUrl = "/images/" + imageName;
            oldLocation.IdTypeLocation = newLocation.IdTypeLocation;

            await UpdateAsync(oldLocation);
        }

        private void DeleteFile(string? imageUrl)
        {
            var imagePath = _environment.WebRootPath + imageUrl;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        private async void InsertFile(IFormFile? file, string imageName)
        {
            var newImagePath = _environment.WebRootPath + "\\images\\";
            if (!Directory.Exists(newImagePath))
            {
                Directory.CreateDirectory(newImagePath);
            }
            using (FileStream stream = System.IO.File.Create(newImagePath + imageName))
            {
                if (file != null)
                {
                    await file.CopyToAsync(stream);
                }
                stream.Flush();
            }
        }
    }
}
