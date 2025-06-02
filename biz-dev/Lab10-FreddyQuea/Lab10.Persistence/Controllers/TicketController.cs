using System.Security.Claims;
using Lab10.Application.DTOs.Ticket;
using Lab10.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Persistence.Controllers;

[Route("api/tickets")]
[ApiController]
[Authorize]
public class TicketController(ITicketService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GetTicketDto>> CreateTicket([FromBody] CreateTicketDto createTicketDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var ticket = await service.CreateTicketAsync(createTicketDto, userId);
        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetTicketDto>> GetTicket([FromRoute] Guid id)
    {
        var ticket = await service.GetTicketByIdAsync(id);
        return Ok(ticket);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetAllTickets()
    {
        var tickets = await service.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateTicketStatus([FromRoute] Guid id,
        [FromBody] UpdateTicketStatusDto updateTicketStatusDto)
    {
        await service.UpdateTicketStatusAsync(id, updateTicketStatusDto);
        return NoContent();
    }
}