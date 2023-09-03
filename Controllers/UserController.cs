using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;
        public UserController(IUserInterface userInterface, IMapper mapper, IConfiguration configuration)
        {
            _userService = userInterface;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("User")]
        //register user
        public async Task<ActionResult<string>> RegisterUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            //hash password
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            var res = await _userService.RegisterUser(user);

            return CreatedAtAction(nameof(RegisterUser), res);
        }
        //Login User
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LogUser user)
        {
            //check if user is registered
            var existingUser = await _userService.GetUserByEmail(user.email);
            if(existingUser == null)
            {
                return NotFound("Invalid Credentials");
            }
            //verify password
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(user.password, existingUser.password);
            if (!isPasswordValid)
            {
                return NotFound("Invalid Credentials");
            }

            //asign token to the user
            var token = CreateToken(existingUser);
            return Ok(token);
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
        //Delete user only by the admin

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserById(id);
            //check i f current user is and admin
            var role = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if(!string.IsNullOrWhiteSpace(role) && role == "Admin")
            {
                var res = await _userService.DeleteUser(user);
                return Ok(res);
            }
            return BadRequest("Admin priviledges are required");
        }
        //create a token
        private string CreateToken(User user)
        {
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecurity:SecretKey")));
            //signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Names", user.Name));
            claims.Add(new Claim("Sub", user.Id.ToString()));
            claims.Add(new Claim("Role", user.Role));
            //generate token
            var TokenGenerated = new JwtSecurityToken(
                _configuration["Tokensecurity:Issuer"],
                _configuration["TokenSecurity:Audience"],
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(TokenGenerated);

            return token;
        }
    }
}
