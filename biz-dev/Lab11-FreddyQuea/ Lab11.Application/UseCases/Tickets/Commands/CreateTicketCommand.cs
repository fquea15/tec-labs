using AutoMapper;
using Lab11.Application.DTOs.Ticket;
using Lab11.Domain.Entities;
using Lab11.Domain.Enums;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Tickets.Commands;

public class CreateTicketCommand : IRequest<GetTicketDto>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid UserId { get; set; }
}

internal sealed class CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateTicketCommand, GetTicketDto>
{
    public async Task<GetTicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.GenericRepository<User>().GetByIdAsync(request.UserId);
        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado");

        var ticket = mapper.Map<Ticket>(request);
        ticket.TicketId = Guid.NewGuid();
        ticket.Status = TicketStatus.Abierto;
        ticket.UserId = request.UserId;

        await unitOfWork.GenericRepository<Ticket>().AddAsync(ticket);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetTicketDto>(ticket);
    }
}