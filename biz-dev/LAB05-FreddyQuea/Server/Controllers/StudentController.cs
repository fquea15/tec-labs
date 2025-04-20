using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Student;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(IStudentService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await service.GetByIdAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudent dto)
    {
        var response = await service.CreateAsync(dto);
        if (response.data != null)
            return CreatedAtAction(nameof(GetById), new { id = response.data.StudentId }, response);
        return BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStudent updateStudent)
    {
        var response = await service.UpdateAsync(id, updateStudent);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await service.DeleteAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }
}