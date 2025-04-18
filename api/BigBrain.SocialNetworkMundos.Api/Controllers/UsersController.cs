using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BigBrain.SocialNetworkMundos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }
            var user = await _userService.CreateUserAsync(request);
            if (user == null)
            {
                return BadRequest("User creation failed.");
            }
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _userService.GetAllUsersAsync();
            if (users == null || users.Length == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpGet("NameOrUsername")]
        public async Task<IActionResult> GetUsersByNameOrUsername([FromQuery] GetUsersRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid search criteria.");
            }
            var users = await _userService.GetUsersByNameOrUsernameAsync(request);
            if (users == null || users.Count == 0) 
            {
                return NotFound("No users found matching the criteria.");
            }
            return Ok(users);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            var user = await _userService.GetUsersByIdAsync(Id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("{Id}")]

        public async Task<IActionResult> UpdateUser([FromRoute] Guid Id, [FromBody] UpdateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user data.");
            }

            var user = await _userService.UpdateUserAsync(Id, request);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteUser([FromRoute] Guid Id)
        {
            var user = await _userService.DeleteUserAsync(Id);
            return Ok();

        }
    }
}


