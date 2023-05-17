using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.RepositoryService;

namespace HueFestivalTicket.Repositories.IRepositories
{
    public interface INewsRepository : IRepository<News>
    {
        public Task<List<News>> GetAllNewsAsync();
        public Task<object> GetNewsPagingAsync(int pageNumber, int pageSize);
        public Task<List<News>> GetAllNewsByAccountAsync(Guid id);
        public Task<News?> GetNewsByIdAsync(Guid id);
        public Task<News?> GetNewsByTitleAsync(string title);
        public Task<News> InsertNewsAsync(NewsDTO news, Guid id);
        public Task UpdateNewsAsync(News oldNews, NewsDTO newNews);
        public Task DeleteNewsByIdAsync(News news);
        public Task<bool> CheckNewsTitle(string title, Guid id);
    }
}
