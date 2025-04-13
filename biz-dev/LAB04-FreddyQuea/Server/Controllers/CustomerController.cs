using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Customer;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await service.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var customer = await service.GetByIdAsync(id);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomer customer)
    {
        var result = await service.AddAsync(customer);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCustomer customer)
    {
        var result = await service.UpdateAsync(id, customer);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await service.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}