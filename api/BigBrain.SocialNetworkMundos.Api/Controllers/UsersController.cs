using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using BigBrain.SocialNetworkMundos.Domain.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBrain.SocialNetworkMundos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserContextService _userContextService;
        private readonly IImageValidationService _imageValidationService;
        public UsersController(IUserService userService, IUserContextService userContextService, IImageValidationService imageValidationService)
        {
            _userService = userService;
            _userContextService = userContextService;
            _imageValidationService = imageValidationService;
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
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (request == null)
                return BadRequest("Invalid user data.");

            var userId = _userContextService.GetUserId();
            if (userId == null)
                return Unauthorized("ID do usuário não encontrado no token.");

            var updatedUser = await _userService.UpdateUserAsync(userId.Value, request);
            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }
        [Authorize]
        [HttpDelete]

        public async Task<IActionResult> DeleteUser()
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
                return Unauthorized("ID do usuário não encontrado no token.");



            var user = await _userService.DeleteUserAsync(userId.Value);
            return Ok();

        }
        [Authorize]
        [HttpPost("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (!_imageValidationService.IsValidImage(file, out var error))
            {
                return BadRequest(error);
            }

            var userId = _userContextService.GetUserId();


            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{userId}{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profile-pictures", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/profile-pictures/{fileName}";

            await _userService.UpdateProfilePictureAsync(userId.Value, imageUrl);

            return Ok(new { imageUrl });
        }

    }
}


