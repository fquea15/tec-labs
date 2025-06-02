using Lab11.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Persistence.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
    {
        var query = new GetUserByIdQuery { UserId = userId };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}