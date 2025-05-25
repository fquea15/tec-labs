using Lab10.Application.DTOs.User;
using Lab10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> GetUser([FromRoute] Guid id)
    {
        var user = await service.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
    {
        var users = await service.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}/tickets")]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> GetUserWithTickets([FromRoute] Guid id)
    {
        var user = await service.GetUserWithTicketsAsync(id);
        return Ok(user);
    }
}