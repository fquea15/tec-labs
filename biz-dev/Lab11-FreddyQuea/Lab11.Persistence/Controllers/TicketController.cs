using System.Security.Claims;
using Lab11.Application.DTOs.Ticket;
using Lab11.Application.UseCases.Tickets.Commands;
using Lab11.Application.UseCases.Tickets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Persistence.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
    {
        var command = new CreateTicketCommand
        {
            Title = createTicketDto.Title,
            Description = createTicketDto.Description,
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        };
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetTicket), new { id = result.TicketId }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTicket([FromRoute] Guid id)
    {
        var query = new GetTicketByIdQuery() { Id = id };
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var query = new GetAllTicketsQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateTicketStatus(Guid id, [FromBody] UpdateTicketStatusDto updateTicketStatusDto)
    {
        var command = new UpdateTicketStatusCommand
        {
            TicketId = id,
            Status = updateTicketStatusDto.Status,
            CloseTicket = updateTicketStatusDto.CloseTicket
        };
        await mediator.Send(command);
        return Ok();
    }
}