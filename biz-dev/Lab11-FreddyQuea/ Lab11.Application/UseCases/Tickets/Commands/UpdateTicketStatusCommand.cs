using Lab11.Domain.Entities;
using Lab11.Domain.Enums;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Tickets.Commands;

public class UpdateTicketStatusCommand : IRequest<Unit>
{
    public Guid TicketId { get; init; }
    public string Status { get; init; } = string.Empty;
    public bool CloseTicket { get; init; }
}

internal class UpdateTicketStatusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTicketStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>().GetByIdAsync(request.TicketId);
        if (ticket == null)
            throw new InvalidOperationException("Ticket no encontrado");

        if (!Enum.TryParse<TicketStatus>(request.Status, true, out var status))
            throw new InvalidOperationException("Estado no permitido");

        ticket.Status = status;
        ticket.ClosedAt = request.CloseTicket switch
        {
            true when status == TicketStatus.Cerrado => DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            false when status != TicketStatus.Cerrado => null,
            _ => ticket.ClosedAt
        };

        await unitOfWork.GenericRepository<Ticket>().UpdateAsync(ticket);
        await unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}