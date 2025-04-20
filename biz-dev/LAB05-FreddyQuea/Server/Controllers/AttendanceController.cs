using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[Route("api/attendances")]
[ApiController]
public class AttendanceController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult Put([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        return Ok();
    }
}