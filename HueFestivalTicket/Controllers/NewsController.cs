﻿using HueFestivalTicket.Contexts;
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
        private readonly ApplicationDbContext _context;
        private readonly INewsRepository _newsRepository;

        public NewsController(ApplicationDbContext context, INewsRepository newsRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            return await _newsRepository.GetAllNewsAsync();
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(Guid id)
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return news;
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<News>>> GetNewsByAccount()
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            return await _newsRepository.GetAllNewsByAccountAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));
        }
    }
}
