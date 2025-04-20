using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Course;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await service.GetAllAsync();
        if (response.success)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await service.GetByIdAsync(id);
        if (response.success)
            return Ok(response);
        return NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourse createCourse)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos del curso inválidos");

        var response = await service.CreateAsync(createCourse);
        if (response.success)
            return CreatedAtAction(nameof(GetById), new { id = response.data?.CourseId }, response);

        return BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCourse updateCourse)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos del curso inválidos");

        var response = await service.UpdateAsync(id, updateCourse);
        if (response.success)
            return Ok(response);

        return NotFound(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await service.DeleteAsync(id);
        if (response.success)
            return Ok(response);

        return NotFound(response);
    }
}