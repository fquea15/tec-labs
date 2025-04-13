using Microsoft.AspNetCore.Mvc;
using Server.DTOs.OrderDetail;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/order-details")]
public class OrderDetailController(IOrderDetailService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var details = await service.GetAllAsync();
        return Ok(details);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var detail = await service.GetByIdAsync(id);
        return detail is null ? NotFound() : Ok(detail);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDetail orderDetail)
    {
        var result = await service.AddAsync(orderDetail);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderDetail orderDetail)
    {
        var result = await service.UpdateAsync(id, orderDetail);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await service.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}