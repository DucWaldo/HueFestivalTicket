using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class TypeLocationRepository : RepositoryBase<TypeLocation>, ITypeLocationRepository
    {
        private readonly IWebHostEnvironment _environment;

        public TypeLocationRepository(ApplicationDbContext context, IWebHostEnvironment environment) : base(context)
        {
            _environment = environment;
        }

        public async Task<bool> CheckNameTypeLocation(string name, Guid id)
        {
            var typeLocation = await _dbSet.FirstOrDefaultAsync(tl => tl.Name == name && tl.IdTypeLocation != id);
            if (typeLocation != null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteTypeLocationAsync(TypeLocation typeLocation)
        {
            DeleteFile(typeLocation.ImageUrl);
            await DeleteAsync(typeLocation);
        }

        public async Task<List<TypeLocation>> GetAllTypeLocationAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TypeLocation?> GetTypeLocationByIdAsync(Guid id)
        {
            var typeLocation = await _dbSet.FirstOrDefaultAsync(tl => tl.IdTypeLocation == id);
            return typeLocation;
        }

        public async Task<TypeLocation?> GetTypeLocationByNameAsync(string name)
        {
            var typeLocation = await _dbSet.FirstOrDefaultAsync(tl => tl.Name == name);
            return typeLocation;
        }

        public async Task<TypeLocation> InsertTypeLocationAsync(TypeLocationDTO typeLocation)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(typeLocation.ImageUrl!.FileName);
            InsertFile(typeLocation.ImageUrl, imageName);

            var newTypeLocation = new TypeLocation
            {
                Name = typeLocation.Name,
                ImageUrl = "/images/" + imageName,
            };
            await InsertAsync(newTypeLocation);
            return newTypeLocation;
        }

        public async Task UpdateTypeLocationAsync(TypeLocation oldTypeLocation, TypeLocationDTO newTypeLocation)
        {
            DeleteFile(oldTypeLocation.ImageUrl);
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(newTypeLocation.ImageUrl!.FileName);
            InsertFile(newTypeLocation.ImageUrl, imageName);
            var url = "/images/" + imageName;

            oldTypeLocation.ImageUrl = url;
            oldTypeLocation.Name = newTypeLocation.Name;

            await UpdateAsync(oldTypeLocation);
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
