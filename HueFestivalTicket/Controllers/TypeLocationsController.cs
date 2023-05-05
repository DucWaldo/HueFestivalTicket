using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeLocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITypeLocationRepository _typeLocationRepository;
        private readonly IWebHostEnvironment _environment;

        public TypeLocationsController(ApplicationDbContext context, ITypeLocationRepository typeLocationRepository, IWebHostEnvironment environment)
        {
            _context = context;
            _typeLocationRepository = typeLocationRepository;
            _environment = environment;
        }

        // GET: api/TypeLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLocation>>> GetTypeLocations()
        {
            if (_context.TypeLocations == null)
            {
                return NotFound();
            }
            return await _typeLocationRepository.GetAllTypeLocationAsync();
        }

        // GET: api/TypeLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLocation>> GetTypeLocation(Guid id)
        {
            if (_context.TypeLocations == null)
            {
                return NotFound();
            }
            var typeLocation = await _context.TypeLocations.FindAsync(id);

            if (typeLocation == null)
            {
                return NotFound();
            }

            return typeLocation;
        }

        // PUT: api/TypeLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeLocation(Guid id, [FromForm] TypeLocationDTO typeLocation)
        {
            var check = await _typeLocationRepository.GetTypeLocationByIdAsync(id);
            if (check == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} does not exist"
                });
            }
            DeleteFile(check.ImageUrl);

            if (typeLocation.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(typeLocation.ImageUrl.FileName);
            InsertFile(typeLocation.ImageUrl, imageName);
            var url = "/images/" + imageName;

            check.ImageUrl = url;
            check.Name = typeLocation.Name;

            await _typeLocationRepository.UpdateTypeLocationAsync(check);
            return Ok(new
            {
                Message = "Update Success",
                check
            });
        }

        // POST: api/TypeLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeLocation>> PostTypeLocation([FromForm] TypeLocationDTO typeLocation)
        {
            if (_context.TypeLocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeLocations'  is null.");
            }

            if (typeLocation.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(typeLocation.ImageUrl.FileName);
            InsertFile(typeLocation.ImageUrl, imageName);

            var newTypeLocation = new TypeLocation
            {
                Name = typeLocation.Name,
                ImageUrl = "/images/" + imageName,
            };

            await _typeLocationRepository.InsertTypeLocationAsync(newTypeLocation);

            return Ok(new
            {
                typeLocation
            });
        }

        // DELETE: api/TypeLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeLocation(Guid id)
        {
            if (_context.TypeLocations == null)
            {
                return NotFound();
            }
            var typeLocation = await _typeLocationRepository.GetTypeLocationByIdAsync(id);
            if (typeLocation == null)
            {
                return Ok(new
                {
                    Message = $"Id {id} does not exist"
                });
            }

            await _typeLocationRepository.DeleteTypeLocationAsync(typeLocation);

            return Ok(new
            {
                Message = "Delete Success"
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
