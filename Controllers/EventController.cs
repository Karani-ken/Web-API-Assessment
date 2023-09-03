using AutoMapper;
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
        public EventController( IMapper mapper, IEventInterface eventservice)
        {
            _mapper = mapper;            
            _eventService = eventservice;
        }
        [HttpPost]
        //add event
        public async Task<ActionResult<string>> CreateEvent(AddEvent newEvent)
        {
            var eventToAdd = _mapper.Map<Event>(newEvent);
            var res = await _eventService.CreateEvent(eventToAdd);
            return Ok(res);
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
    }
}
