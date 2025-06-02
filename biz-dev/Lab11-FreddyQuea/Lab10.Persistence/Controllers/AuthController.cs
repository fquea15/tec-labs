using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult SignUp()
    {
        return Ok(new { Message = "User created" });
    }

    [HttpPost("login")]
    public IActionResult SignIn()
    {
        return Ok(new { message = "User logged in" });
    }
}