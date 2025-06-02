using AutoMapper;
using Lab11.Application.DTOs.Response;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.UseCases.Responses.Queries;

public class GetResponsesByTicketIdQuery : IRequest<IEnumerable<GetResponseDto>>
{
    public Guid TicketId { get; set; }
}

internal class GetResponsesByTicketIdQueryHandler : IRequestHandler<GetResponsesByTicketIdQuery, IEnumerable<GetResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetResponsesByTicketIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetResponseDto>> Handle(GetResponsesByTicketIdQuery request, CancellationToken cancellationToken)
    {
        var responses = await _unitOfWork.GenericRepository<Response>()
            .GetAllWithIncludesAsync(query => query
                .Where(r => r.TicketId == request.TicketId)
                .Include(r => r.Responder));
        return _mapper.Map<IEnumerable<GetResponseDto>>(responses);
    }
}