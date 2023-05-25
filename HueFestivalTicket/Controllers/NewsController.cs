using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            return await _newsRepository.GetAllNewsAsync();
        }

        // GET: api/News/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<News>>> GetNewsPaging(int pageNumber, int pageSize)
        {
            var result = await _newsRepository.GetNewsPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(Guid id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);

            if (news == null)
            {
                return Ok(new
                {
                    Message = "This News doesn't exist"
                });
            }

            return news;
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ReporterPolicy")]
        public async Task<IActionResult> PutNews(Guid id, [FromForm] NewsDTO news)
        {
            var oldNews = await _newsRepository.GetNewsByIdAsync(id);
            if (oldNews == null)
            {
                return Ok(new
                {
                    Message = "This News doesn't exist"
                });
            }
            if (news.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }
            if (news.Title == null || await _newsRepository.CheckNewsTitle(news.Title, oldNews.IdNews) == false)
            {
                return Ok(new
                {
                    Message = "Title is empty or already exists"
                });
            }
            if (Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) != oldNews.IdAccount)
            {
                return Ok(new
                {
                    Message = "You don't have permission to update this article"
                });
            }

            await _newsRepository.UpdateNewsAsync(oldNews, news);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/News
        [HttpPost]
        [Authorize(Policy = "ReporterPolicy")]
        public async Task<ActionResult<News>> PostNews([FromForm] NewsDTO news)
        {
            if (news.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }
            if (news.Title == null || await _newsRepository.GetNewsByTitleAsync(news.Title) != null)
            {
                return Ok(new
                {
                    Message = "Title is empty or already exists"
                });
            }

            var result = await _newsRepository.InsertNewsAsync(news, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ReporterPolicy")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var news = await _newsRepository.GetNewsByIdAsync(id);
            if (news == null)
            {
                return Ok(new
                {
                    Message = "This News doesn't exist"
                });
            }

            await _newsRepository.DeleteNewsByIdAsync(news);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }

        // GET: api/News
        [HttpGet("Account")]
        [Authorize(Policy = "ReporterPolicy")]
        public async Task<ActionResult<IEnumerable<News>>> GetNewsByAccount()
        {
            return await _newsRepository.GetAllNewsByAccountAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));
        }
    }
}
