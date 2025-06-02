using Lab10.Application.DTOs.Auth;
using Lab10.Application.DTOs.User;
using Lab10.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<GetUserDto>> SignUp([FromBody] CreateUserDto createUserDto)
    {
        var user = await userService.CreateUserAsync(createUserDto);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult> SignIn([FromBody] LoginDto loginDto)
    {
        var token = await authService.AuthenticateAsync(loginDto);
        return Ok(new { Token = token });
    }
}