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

        public ImageEventsController(ApplicationDbContext context, IWebHostEnvironment environment, IImageEventRepository imageEventRepository)
        {
            _context = context;
            _environment = environment;
            _imageEventRepository = imageEventRepository;
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
                    Message = $"This {id} already exists"
                });
            }
            return File(imageEvent.ImageUrl ?? "", "image/jpeg");
        }

        // PUT: api/ImageEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageEvent(IFormFile file, Guid id)
        {
            var image = await _imageEventRepository.GetImageEventByIdAsync(id);
            if (image == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} not found"
                });
            }
            DeleteFile(image.ImageUrl);

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            InsertFile(file, imageName);

            var url = "/images/" + imageName;
            await _imageEventRepository.UpdateImageEventAsync(id, url);

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
            if (imageEvent.ImageUrl == null || imageEvent.ImageUrl.Count == 0)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            foreach (var file in imageEvent.ImageUrl)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                InsertFile(file, imageName);
                var image = new ImageEvent
                {
                    ImageUrl = "/images/" + imageName,
                    IdEvent = imageEvent.IdEvent
                };
                await _imageEventRepository.InsertImageEventAsync(image);
            }
            return Ok(new
            {
                Message = "Insert Success",
                imageEvent
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
                    Message = $"Id {id} not found"
                });
            }

            DeleteFile(imageEvent.ImageUrl);

            await _imageEventRepository.DeleteImageEventAsync(id);

            var getAll = await _imageEventRepository.GetAllImageEventsAsync();

            return Ok(new
            {
                Message = "Delete Success",
                getAll
            });
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
