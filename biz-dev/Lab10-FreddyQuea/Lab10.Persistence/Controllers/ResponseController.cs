using System.Security.Claims;
using Lab10.Application.DTOs.Response;
using Lab10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/responses")]
[ApiController]
[Authorize]
public class ResponseController(IResponseService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GetResponseDto>> CreateResponse([FromBody] CreateResponseDto createResponseDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var response = await service.CreateResponseAsync(createResponseDto, userId);
        return CreatedAtAction(nameof(CreateResponse), new { id = response.Id }, response);
    }
}