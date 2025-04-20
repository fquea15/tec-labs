using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Subject;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Controllers;

[ApiController]
[Route("api/subjects")]
public class SubjectController(ISubjectService subjectService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetSubjectWithCourse>>>> GetAll()
    {
        var response = await subjectService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetSubjectWithCourse>>> GetById([FromRoute] int id)
    {
        var response = await subjectService.GetByIdAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetSubjectWithCourse>>> Create(
        [FromBody] CreateSubject createSubject)
    {
        var response = await subjectService.CreateAsync(createSubject);
        return CreatedAtAction(nameof(GetById), new { id = response.data?.SubjectId }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetSubjectWithCourse>>> Update([FromRoute] int id,
        [FromBody] UpdateSubject updateSubject)
    {
        var response = await subjectService.UpdateAsync(id, updateSubject);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
    {
        var response = await subjectService.DeleteAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }
}