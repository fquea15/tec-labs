using AutoMapper;
using Lab11.Application.DTOs.Response;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Responses.Commands;

public class CreateResponseCommand : IRequest<GetResponseDto>
{
    public string Message { get; set; } = null!;
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
}

internal class CreateResponseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateResponseCommand, GetResponseDto>
{
    public async Task<GetResponseDto> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>().GetByIdAsync(request.TicketId);
        if (ticket == null)
            throw new InvalidOperationException("Ticket no encontrado");

        var user = await unitOfWork.GenericRepository<User>().GetByIdAsync(request.UserId);
        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado");

        var response = mapper.Map<Response>(request);
        response.ResponseId = Guid.NewGuid();

        await unitOfWork.GenericRepository<Response>().AddAsync(response);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetResponseDto>(response);
    }
}