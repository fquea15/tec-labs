using LAB08_FreddyQuea.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_FreddyQuea.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("exercise3/{orderId:int}")]
    public async Task<IActionResult> GetOrderDetailsByOrderId([FromRoute] int orderId)
    {
        var details = await orderService.GetOrderDetailsByOrderIdAsync(orderId);
        return Ok(details);
    }
    
    [HttpGet("exercise4/{orderId:int}")]
    public async Task<IActionResult> GetTotalQuantityByOrderId(int orderId)
    {
        var total = await orderService.GetTotalQuantityByOrderIdAsync(orderId);
        return Ok(new { total });
    }
    
    [HttpGet("exercise6/{date:datetime}")]
    public async Task<IActionResult> GetOrdersAfterDate([FromRoute] DateTime date)
    {
        var orders = await orderService.GetOrdersAfterDateAsync(date);
        return Ok(orders);
    }
    
    [HttpGet("exercise10")]
    public async Task<IActionResult> GetAllOrderDetails()
    {
        var details = await orderService.GetAllOrderDetailsAsync();
        return Ok(details);
    }
    
    [HttpGet("with-details")]
    public async Task<IActionResult> GetOrderWithDetails()
    {
        var orders = await orderService.GetOrderWithDetailsAsync();
        return Ok(orders);
    }
}





