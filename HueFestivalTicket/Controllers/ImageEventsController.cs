using HueFestivalTicket.Contexts;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await _context.ImageEvents.ToListAsync();
        }

        // GET: api/ImageEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageEvent>> GetImageEvent(Guid id)
        {
            if (_context.ImageEvents == null)
            {
                return NotFound();
            }
            var imageEvent = await _context.ImageEvents.FindAsync(id);

            if (imageEvent == null)
            {
                return NotFound();
            }

            return imageEvent;
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
            /*var imagePath = _environment.WebRootPath + image.ImageUrl;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }*/

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            InsertFile(file, imageName);
            /*var newImagePath = _environment.WebRootPath + "\\images\\";
            if (!Directory.Exists(newImagePath))
            {
                Directory.CreateDirectory(newImagePath);
            }
            using (FileStream stream = System.IO.File.Create(newImagePath + imageName))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }*/

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
        public async Task<ActionResult<ImageEvent>> PostImageEvent(List<IFormFile> files, Guid id)
        {
            if (_context.ImageEvents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ImageEvents'  is null.");
            }
            if (files == null || files.Count == 0)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            foreach (var file in files)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                /*
                var imagePath = _environment.WebRootPath + "\\images\\";
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }
                using (FileStream stream = System.IO.File.Create(imagePath + imageName))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                }
                */
                InsertFile(file, imageName);
                var image = new ImageEvent
                {
                    ImageUrl = "/images/" + imageName,
                    IdEvent = id
                };
                await _imageEventRepository.InsertImageEventAsync(image);
            }
            return Ok(new
            {
                Message = "Insert Success",

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
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Delete Success",

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
