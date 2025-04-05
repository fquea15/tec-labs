using Lab03_FreddyQuea.DTOs.User;
using Lab03_FreddyQuea.Models;
using Lab03_FreddyQuea.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab03_FreddyQuea.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var data = await service.GetAllAsync();
    return data.Any() ? Ok(data) : NotFound(data);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> Get([FromRoute] int id)
  {
    var data = await service.GetByIdAsync(id);
    return Ok(data);
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateUser user)
  {
    var result = await service.AddAsync(user);
    return result.Success ? Ok(result) : BadRequest(result);
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateUser user)
  {
    var result = await service.UpdateAsync(user);
        return result.Success ? Ok(result) : BadRequest(result);
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var result = await service.DeleteAsync(id);
    return result.Success ? Ok(result) : BadRequest(result);
  }
}