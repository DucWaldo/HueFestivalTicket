﻿using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeLocationsController : ControllerBase
    {
        private readonly ITypeLocationRepository _typeLocationRepository;
        private readonly IWebHostEnvironment _environment;

        public TypeLocationsController(ITypeLocationRepository typeLocationRepository, IWebHostEnvironment environment)
        {
            _typeLocationRepository = typeLocationRepository;
            _environment = environment;
        }

        // GET: api/TypeLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLocation>>> GetTypeLocations()
        {
            return await _typeLocationRepository.GetAllTypeLocationAsync();
        }

        // GET: api/TypeLocations/Paging
        [HttpGet("Paging")]
        public async Task<ActionResult<IEnumerable<TypeLocation>>> GetTypeLocationPaging(int pageNumber, int pageSize)
        {
            var result = await _typeLocationRepository.GetTypeLocationPagingAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/TypeLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLocation>> GetTypeLocation(Guid id)
        {
            var typeLocation = await _typeLocationRepository.GetTypeLocationByIdAsync(id);
            if (typeLocation == null)
            {
                return Ok(new
                {
                    Message = "This Type Location doesn't exist"
                });
            }

            return typeLocation;
        }

        // PUT: api/TypeLocations/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> PutTypeLocation(Guid id, [FromForm] TypeLocationDTO typeLocation)
        {
            var oldTypeLocation = await _typeLocationRepository.GetTypeLocationByIdAsync(id);
            if (oldTypeLocation == null)
            {
                return Ok(new
                {
                    Message = "This Type Location not found"
                });
            }
            if (typeLocation.Name == null || await _typeLocationRepository.CheckNameTypeLocation(typeLocation.Name, oldTypeLocation.IdTypeLocation) == false)
            {
                return Ok(new
                {
                    Message = "Name Type Location is empty or already exist"
                });
            }
            if (typeLocation.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            await _typeLocationRepository.UpdateTypeLocationAsync(oldTypeLocation, typeLocation);
            return Ok(new
            {
                Message = "Update Success"
            });
        }

        // POST: api/TypeLocations
        [HttpPost]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult<TypeLocation>> PostTypeLocation([FromForm] TypeLocationDTO typeLocation)
        {
            if (typeLocation.Name == null || await _typeLocationRepository.GetTypeLocationByNameAsync(typeLocation.Name) != null)
            {
                return Ok(new
                {
                    Message = "Name Type Location is empty or already exist"
                });
            }
            if (typeLocation.ImageUrl == null)
            {
                return Ok(new
                {
                    Message = "Please choose image"
                });
            }

            var result = await _typeLocationRepository.InsertTypeLocationAsync(typeLocation);

            return Ok(new
            {
                Message = "Insert Success",
                result
            });
        }

        // DELETE: api/TypeLocations/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> DeleteTypeLocation(Guid id)
        {
            var typeLocation = await _typeLocationRepository.GetTypeLocationByIdAsync(id);
            if (typeLocation == null)
            {
                return Ok(new
                {
                    Message = "This Type Location not found"
                });
            }

            await _typeLocationRepository.DeleteTypeLocationAsync(typeLocation);

            return Ok(new
            {
                Message = "Delete Success"
            });
        }
    }
}
