using Lab03_FreddyQuea.DTOs.Task;
using Lab03_FreddyQuea.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab03_FreddyQuea.Controllers;

[Route("api/tasks")]
[ApiController]
public class TaskController(ITaskService service) : ControllerBase
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
  public async Task<IActionResult> Post([FromBody] CreateTask user)
  {
    var result = await service.AddAsync(user);
    return result.Success ? Ok(result) : BadRequest(result);
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateTask user)
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