using Microsoft.AspNetCore.Mvc;

namespace Lab2_FreddyQuea.Controllers;

[ApiController]
[Route("home")]
public class ApiController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok(new {message = "hello world!"});
  }
}