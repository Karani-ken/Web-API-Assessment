using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;
using Web_API_Assessment.Services.IServices;

namespace Web_API_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController_ : ControllerBase
    {
        private readonly IUserInterface _userService;
        private readonly IMapper _mapper;
        private readonly IEventInterface _eventService;
        public UserController_(IUserInterface userService, IMapper mapper, IEventInterface eventservice)
        {
            _mapper = mapper;
            _userService = userService;
            _eventService= eventservice;
        }

        [HttpPost("User")]
        //register user
        public async Task<ActionResult<string>> RegisterUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            var res = await _userService.RegisterUser(user);

            return CreatedAtAction(nameof(RegisterUser), res);
        }
        [HttpGet("User")]
        public async Task<ActionResult<IEnumerable<User>>> getAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        [HttpPost("Event")]
        //add event
        public async Task<ActionResult<string>> CreateEvent(AddEvent newEvent)
        {
            var eventToAdd = _mapper.Map<Event>(newEvent);
            var res = await _eventService.CreateEvent(eventToAdd);
            return Ok(res);
        }
        //get Event by location
        [HttpGet("Event")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByLocation(string? location)
        {
            var Events = await _eventService.basedOnLocation(location);
            return Events.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetEventCapacity(Guid id)
        {
            var SelectedEvent = await _eventService.RemainingSlots(id);
            if(SelectedEvent == null)
            {
                return BadRequest("Event Does not Exist");
            }
            var Capacity = SelectedEvent.Capacity;
            var BookedSlots = SelectedEvent.Users.Count();
            var remainingSlots = Capacity - BookedSlots;

            return remainingSlots;
        }
    }
}
