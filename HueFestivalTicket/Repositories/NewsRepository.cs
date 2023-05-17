using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using HueFestivalTicket.Repositories.RepositoryService;
using Microsoft.EntityFrameworkCore;

namespace HueFestivalTicket.Repositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        private readonly IWebHostEnvironment _environment;
        public NewsRepository(ApplicationDbContext context, IWebHostEnvironment environment) : base(context)
        {
            _environment = environment;
        }

        public async Task<bool> CheckNewsTitle(string title, Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(n => n.Title == title && n.IdNews != id);
            if (result != null)
            {
                return false;
            }
            return true;
        }

        public async Task DeleteNewsByIdAsync(News news)
        {
            DeleteFile(news.ImageUrl);
            await DeleteAsync(news);
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            return await GetAllWithIncludesAsync(n => n.Account!.Role!);
        }

        public async Task<List<News>> GetAllNewsByAccountAsync(Guid id)
        {
            var list = await GetAllWithIncludesAsync(n => n.Account!.Role!);
            List<News> result = list.Where(n => n.IdAccount == id).ToList();
            return result;
        }

        public async Task<News?> GetNewsByIdAsync(Guid id)
        {
            var result = await _dbSet.Include(n => n.Account!.Role).FirstOrDefaultAsync(n => n.IdNews == id);
            return result;
        }

        public async Task<News?> GetNewsByTitleAsync(string title)
        {
            var result = await _dbSet.FirstOrDefaultAsync(n => n.Title == title);
            return result;
        }

        public async Task<object> GetNewsPagingAsync(int pageNumber, int pageSize)
        {
            List<News> data = await GetPage(pageNumber, pageSize, n => n.TimeCreate, n => n.Account!.Role!);
            return ReturnGetPage(data, pageNumber, pageSize);
        }

        public async Task<News> InsertNewsAsync(NewsDTO news, Guid id)
        {

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(news.ImageUrl!.FileName);
            InsertFile(news.ImageUrl, imageName);

            var newNews = new News
            {
                Title = news.Title,
                Content = news.Content,
                TimeCreate = DateTime.UtcNow,
                ImageUrl = "/images/" + imageName,
                IdAccount = id
            };

            await InsertAsync(newNews);
            return newNews;
        }

        public async Task UpdateNewsAsync(News oldNews, NewsDTO newNews)
        {
            DeleteFile(oldNews.ImageUrl);
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(newNews.ImageUrl!.FileName);
            InsertFile(newNews.ImageUrl, imageName);

            oldNews.Title = newNews.Title;
            oldNews.Content = newNews.Content;
            oldNews.TimeCreate = DateTime.UtcNow;
            oldNews.ImageUrl = "/images/" + imageName;

            await UpdateAsync(oldNews);
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
