using Lab11.Application.DTOs.Auth;
using Lab11.Application.DTOs.User;
using Lab11.Application.UseCases.Auth.Queries;
using Lab11.Application.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Persistence.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] CreateUserDto createUserDto)
    {
        var command = new CreateUserCommand
        {
            Username = createUserDto.Username,
            Password = createUserDto.Password,
            Email = createUserDto.Email
        };
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromBody] LoginDto loginDto)
    {
        var query = new LoginQuery
        {
            Username = loginDto.Username,
            Password = loginDto.Password
        };
        var token = await mediator.Send(query);
        return Ok(new { token });
    }
}