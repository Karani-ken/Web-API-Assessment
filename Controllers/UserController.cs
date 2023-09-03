using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API_Assessment.Models;
using Web_API_Assessment.Requests;
using Web_API_Assessment.Services.IServices;

namespace Web_API_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUserInterface _userService;
        public UserController(IUserInterface userInterface, IMapper mapper)
        {
            _userService = userInterface;
            _mapper = mapper;
        }

        [HttpPost("User")]
        //register user
        public async Task<ActionResult<string>> RegisterUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            var res = await _userService.RegisterUser(user);

            return CreatedAtAction(nameof(RegisterUser), res);
        }
        //get users
        [HttpGet("User")]
        public async Task<ActionResult<IEnumerable<User>>> getAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        //Book Event
        [HttpPut("Book Event")]
        public async Task<ActionResult<string>> BookEvent(BookEvent book)
        {
            try
            {
                var res = await _userService.BookEvent(book);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
