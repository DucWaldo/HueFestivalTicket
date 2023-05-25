using HueFestivalTicket.Data;
using HueFestivalTicket.Models;
using HueFestivalTicket.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HueFestivalTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageEventsController : ControllerBase
    {
        private readonly IImageEventRepository _imageEventRepository;
        private readonly IEventRepository _eventRepository;

        public ImageEventsController(IImageEventRepository imageEventRepository, IEventRepository eventRepository)
        {
            _imageEventRepository = imageEventRepository;
            _eventRepository = eventRepository;
        }

        // GET: api/ImageEvents
        [HttpGet]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<IEnumerable<ImageEvent>>> GetImageEvents()
        {
            return await _imageEventRepository.GetAllImageEventsAsync();
        }

        // GET: api/ImageEvents
        [HttpGet("ImageEventsWithIdEvent")]
        public async Task<ActionResult<IEnumerable<ImageEvent>>> GetImageEventsWithIdEvent(Guid idEvent)
        {
            var @event = await _eventRepository.GetEventByIdAsync(idEvent);
            if (@event == null)
            {
                return Ok(new
                {
                    Message = "This event doesn't exist"
                });
            }
            var result = await _imageEventRepository.GetImageEventsWithIdEventAsync(@event.IdEvent);
            return Ok(new
            {
                result
            });
        }

        // GET: api/ImageEvents/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<ImageEvent>> GetImageEventById(Guid id)
        {
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
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerOrStaff")]
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
        [HttpPost]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<ActionResult<ImageEvent>> PostImageEvent([FromForm] ImageEventDTO imageEvent)
        {
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
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> DeleteImageEvent(Guid id)
        {
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
