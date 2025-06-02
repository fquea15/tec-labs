using Lab10.Application.DTOs.Role;
using Lab10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/roles")]
[ApiController]
[Authorize]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GetRoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto)
    {
        var role = await roleService.CreateRoleAsync(createRoleDto);
        return CreatedAtAction(nameof(GetAllRoles), new { roleId = role.Id }, role);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetAllRoles()
    {
        var roles = await roleService.GetAllRolesAsync();
        return Ok(roles);
    }
}