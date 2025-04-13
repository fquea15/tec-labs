using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Address;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressController(IAddressService service) : ControllerBase
{
    // GET: api/Address
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var addresses = await service.GetAllAsync();
        return Ok(addresses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var address = await service.GetByIdAsync(id);
        return address is null ? NotFound() : Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAddress address)
    {
        var result = await service.AddAsync(address);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAddress address)
    {
        var result = await service.UpdateAsync(id, address);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await service.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}