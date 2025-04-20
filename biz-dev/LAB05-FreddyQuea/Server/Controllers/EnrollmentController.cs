using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Enrollment;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentController(IEnrollmentService enrollmentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetEnrollment>>>> GetAll()
    {
        var response = await enrollmentService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetEnrollment>>> GetById([FromRoute] int id)
    {
        var response = await enrollmentService.GetByIdAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetEnrollment>>> Create(CreateEnrollment createEnrollment)
    {
        var response = await enrollmentService.CreateAsync(createEnrollment);
        return CreatedAtAction(nameof(GetById), new { id = response.data?.EnrollmentId }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GetEnrollment>>> Update([FromRoute] int id,
        [FromBody] UpdateEnrollment updateEnrollment)
    {
        var response = await enrollmentService.UpdateAsync(id, updateEnrollment);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete([FromRoute] int id)
    {
        var response = await enrollmentService.DeleteAsync(id);
        if (!response.success) return NotFound(response);
        return Ok(response);
    }
}