using AutoMapper;
using Lab11.Application.DTOs.Ticket;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.UseCases.Tickets.Queries;

public class GetTicketByIdQuery : IRequest<GetTicketDto>
{
    public Guid Id { get; set; }
}

internal class GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetTicketByIdQuery, GetTicketDto>
{
    public async Task<GetTicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>()
            .GetByIdWithIncludesAsync(request.Id, query => query.Include(t => t.Responses));
        if (ticket == null)
            throw new InvalidOperationException("Ticket no encontrado");

        return mapper.Map<GetTicketDto>(ticket);
    }
}