using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Payment;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await paymentService.GetAllAsync();
        return Ok(payments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var payment = await paymentService.GetByIdAsync(id);
        return payment is null ? NotFound() : Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePayment payment)
    {
        var result = await paymentService.AddAsync(payment);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePayment payment)
    {
        var result = await paymentService.UpdateAsync(id, payment);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await paymentService.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}