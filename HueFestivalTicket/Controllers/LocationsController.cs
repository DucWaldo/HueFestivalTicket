using HueFestivalTicket.Contexts;
using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILocationRepository _locationRepository;
        private readonly ITypeLocationRepository _typeLocationRepository;

        public LocationsController(ApplicationDbContext context, ILocationRepository locationRepository, ITypeLocationRepository typeLocationRepository)
        {
            _context = context;
            _locationRepository = locationRepository;
            _typeLocationRepository = typeLocationRepository;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _locationRepository.GetAllLocationsAsync();
        }

        // GET: api/Locations/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationPaging(int pageNumber, int pageSize)
        {
            var result = await _locationRepository.GetLocationPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(Guid id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);

            if (location == null)
            {
                return Ok(new
                {
                    Message = "This Location not found"
                });
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(Guid id, [FromForm] LocationDTO newLocation)
        {
            var oldLocation = await _locationRepository.GetLocationByIdAsync(id);
            if (oldLocation == null)
            {
                return Ok(new
                {
                    Message = "This Location not found"
                });
            }
            if (await _typeLocationRepository.GetTypeLocationByIdAsync(newLocation.IdTypeLocation) == null)
            {
                return Ok(new
                {
                    Message = "Type Location doesn't exist"
                });
            }
            await _locationRepository.UpdateLocationAsync(oldLocation, newLocation);

            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation([FromForm] LocationDTO location)
        {
            if (_locationRepository.CheckImage(location.ImageUrl) == false)
            {
                return Ok(new
                {
                    Message = "Please choose image/picture"
                });
            }

            if (await _typeLocationRepository.GetTypeLocationByIdAsync(location.IdTypeLocation) == null)
            {
                return Ok(new
                {
                    Message = "Type Location doesn't exist"
                });
            }

            var result = await _locationRepository.InsertLocationAsync(location);

            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null)
            {
                return Ok(new
                {
                    Message = "This Location not found"
                });
            }

            await _locationRepository.DeleteLocationAsync(location);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
