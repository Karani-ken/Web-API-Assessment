using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;
using Web_API_Assessment.Services.IServices;

namespace Web_API_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IEventInterface _eventService;
        public EventController(IMapper mapper, IEventInterface eventservice)
        {
            _mapper = mapper;
            _eventService = eventservice;
        }
        [HttpPost]
        //add event by an admin
        [Authorize]
        public async Task<ActionResult<string>> CreateEvent(AddEvent newEvent)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if (!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var eventToAdd = _mapper.Map<Event>(newEvent);
                var res = await _eventService.CreateEvent(eventToAdd);
                return Ok(res);
            }
            return BadRequest("You are not an admin");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> getAllEvents()
        {
            var Events = await _eventService.GetAllEvents();
            return Ok(Events);
        }
        //get Event by location
        [HttpGet("location")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByLocation(string? location)
        {
            var Events = await _eventService.basedOnLocation(location);
            return Events.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetEventCapacity(Guid id)
        {
            var SelectedEvent = await _eventService.GetEventById(id);
            if (SelectedEvent == null)
            {
                return BadRequest("Event Does not Exist");
            }
            var Capacity = SelectedEvent.Capacity;
            var BookedSlots = SelectedEvent.Users.Count;
            var remainingSlots = Capacity - BookedSlots;

            return BookedSlots;
        }
        //delete event by admin
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteEvent(Guid id)
        {
            var EventToDelete = await _eventService.GetEventById(id);
            //check if user if admin
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if(!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var res = await _eventService.DeleteEvent(EventToDelete);
                return Ok(res);
            }
            return BadRequest("Admin Permision Required");
        }
        //update Event by admin
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateEvent(Guid id, AddEvent eventUpdate)
        {
            var EventToUpdate = await _eventService.GetEventById(id);
            if(EventToUpdate == null)
            {
                return NotFound("Event does not exist");
            }
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if(!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
               var UpdatedEvent = _mapper.Map(eventUpdate, EventToUpdate);

                var res = await _eventService.UpdateEvent(UpdatedEvent);
                return Ok(res);
            }
            return BadRequest("You are not admin");
        }
    }
}
