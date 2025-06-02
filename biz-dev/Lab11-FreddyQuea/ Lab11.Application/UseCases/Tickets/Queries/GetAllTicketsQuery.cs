using AutoMapper;
using Lab11.Application.DTOs.Ticket;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.UseCases.Tickets.Queries;

public class GetAllTicketsQuery : IRequest<IEnumerable<GetTicketDto>>
{
}

internal class GetAllTicketsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllTicketsQuery, IEnumerable<GetTicketDto>>
{
    public async Task<IEnumerable<GetTicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await unitOfWork.GenericRepository<Ticket>()
            .GetAllWithIncludesAsync(query => query.Include(t => t.Responses));
        return mapper.Map<IEnumerable<GetTicketDto>>(tickets);
    }
}