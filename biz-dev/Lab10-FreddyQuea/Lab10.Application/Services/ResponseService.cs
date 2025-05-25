using AutoMapper;
using Lab10.Application.DTOs.Response;
using Lab10.Application.Interfaces;
using Lab10.Domain.Entities;
using Lab10.Domain.Interfaces;

namespace Lab10.Application.Services;

public class ResponseService(IUnitOfWork unitOfWork, IMapper mapper) : IResponseService
{
    public async Task<GetResponseDto> CreateResponseAsync(CreateResponseDto createResponseDto, Guid userId)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>().GetByIdAsync(createResponseDto.TicketId);
        if (ticket == null)
            throw new Exception("Ticket no encontrado");

        var user = await unitOfWork.GenericRepository<User>().GetByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuario no encontrado");

        var response = mapper.Map<Response>(createResponseDto);
        response.Id = Guid.NewGuid();
        response.ResponderId = userId;

        await unitOfWork.GenericRepository<Response>().AddAsync(response);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetResponseDto>(response);
    }
}