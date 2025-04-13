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
    }
}
