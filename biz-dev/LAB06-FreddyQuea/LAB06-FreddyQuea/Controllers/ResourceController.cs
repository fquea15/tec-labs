using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB06_FreddyQuea.Controllers;

[ApiController]
[Route("api/resources")]
public class ResourceController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult GetPublicResource()
    {
        return Ok(new { message = "Este recurso es público" });
    }
    
    [Authorize]
    [HttpGet("protected")]
    public IActionResult GetProtectedResource()
    {
        return Ok(new { message = "Este recurso está protegido, pero cualquier usuario autenticado puede verlo" });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult GetAdminResource()
    {
        return Ok(new { message = "Este recurso es solo para administradores" });
    }
    
    [Authorize(Policy = "UserOnly")]
    [HttpGet("user")]
    public IActionResult GetUserResource()
    {
        return Ok(new { message = "Este recurso es solo para usuarios regulares" });
    }
}