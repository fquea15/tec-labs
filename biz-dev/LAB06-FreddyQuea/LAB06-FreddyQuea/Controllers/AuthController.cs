using LAB06_FreddyQuea.DTOs.Auth;
using LAB06_FreddyQuea.Services.Interfaces;
using LAB06_FreddyQuea.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LAB06_FreddyQuea.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var response = await authService.RegisterAsync(request);

        if (response.success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        if (response.success)
        {
            return Ok(response);
        }

        return Unauthorized(response);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var response = await authService.ChangePasswordAsync(request);

        if (response.success)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}