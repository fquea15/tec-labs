using Lab11.Application.DTOs.Role;
using Lab11.Application.UseCases.Roles.Commands;
using Lab11.Application.UseCases.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Persistence.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        var command = new CreateRoleCommand() { Name = createRoleDto.Name };
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRole), new { id = result.RoleId }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var query = new GetRoleByIdQuery() { Id = id };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var query = new GetAllRolesQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}