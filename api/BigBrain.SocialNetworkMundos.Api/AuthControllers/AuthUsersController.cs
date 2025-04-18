using BigBrain.SocialNetworkMundos.Application.Services;
namespace BigBrain.SocialNetworkMundos.Domain.Models.Requests;
using BigBrain.SocialNetworkMundos.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
 

    public AuthController(
        IUserService userService 
       )
    {
        _userService = userService; 
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _userService.LoginAsync(request.Email, request.Password);
        return token == null ? Unauthorized() : Ok(new { Token = token });
    }
}
