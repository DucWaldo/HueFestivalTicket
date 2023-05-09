using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageEventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IImageEventRepository _imageEventRepository;
        private readonly IEventRepository _eventRepository;

        public ImageEventsController(ApplicationDbContext context, IWebHostEnvironment environment, IImageEventRepository imageEventRepository, IEventRepository eventRepository)
        {
            _context = context;
            _environment = environment;
            _imageEventRepository = imageEventRepository;
            _eventRepository = eventRepository;
        }

        // GET: api/ImageEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageEvent>>> GetImageEvents()
        {
            if (_context.ImageEvents == null)
            {
                return NotFound();
            }
            return await _imageEventRepository.GetAllImageEventsAsync();
        }

        // GET: api/ImageEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageEvent>> GetImageEventById(Guid id)
        {
            if (_context.ImageEvents == null)
            {
                return NotFound();
            }
            var imageEvent = await _imageEventRepository.GetImageEventByIdAsync(id);

            if (imageEvent == null)
            {
                return Ok(new
                {
                    Message = "This Image Event not found"
                });
            }
            return File(imageEvent.ImageUrl ?? "", "image/jpeg");
        }

        // PUT: api/ImageEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageEvent(IFormFile file, Guid id)
        {

            var oldImageEvent = await _imageEventRepository.GetImageEventByIdAsync(id);
            if (oldImageEvent == null)
            {
                return Ok(new
                {
                    Message = "This Image Event not found"
                });
            }
            if (await _eventRepository.GetEventByIdAsync(oldImageEvent.IdEvent) == null)
            {
                return Ok(new
                {
                    Message = "This Event doesn't exist"
                });
            }

            await _imageEventRepository.UpdateImageEventAsync(oldImageEvent, file);

            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/ImageEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageEvent>> PostImageEvent([FromForm] ImageEventDTO imageEvent)
        {
            if (_context.ImageEvents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ImageEvents'  is null.");
            }

            if (await _eventRepository.GetEventByIdAsync(imageEvent.IdEvent) == null)
            {
                return Ok(new
                {
                    Message = "This Event doesn't exist"
                });
            }

            if (imageEvent.ImageUrl == null || imageEvent.ImageUrl.Count == 0)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            var result = await _imageEventRepository.InsertImageEventAsync(imageEvent);
            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/ImageEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageEvent(Guid id)
        {
            if (_context.ImageEvents == null)
            {
                return NotFound();
            }
            var imageEvent = await _imageEventRepository.GetImageEventByIdAsync(id);
            if (imageEvent == null)
            {
                return Ok(new
                {
                    Message = "This Image Event not found"
                });
            }

            await _imageEventRepository.DeleteImageEventAsync(imageEvent);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
