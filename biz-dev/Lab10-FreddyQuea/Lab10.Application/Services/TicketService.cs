using AutoMapper;
using Lab10.Application.DTOs.Ticket;
using Lab10.Application.Interfaces;
using Lab10.Domain.Entities;
using Lab10.Domain.Enum;
using Lab10.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Application.Services;

public class TicketService(IUnitOfWork unitOfWork, IMapper mapper) : ITicketService
{
    public async Task<GetTicketDto> CreateTicketAsync(CreateTicketDto createTicketDto, Guid userId)
    {
        var user = await unitOfWork.GenericRepository<User>().GetByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuario no encontrado");

        var ticket = mapper.Map<Ticket>(createTicketDto);
        ticket.Id = Guid.NewGuid();
        ticket.UserId = userId;
        ticket.Status = TicketStatus.Abierto;

        await unitOfWork.GenericRepository<Ticket>().AddAsync(ticket);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetTicketDto>(ticket);
    }

    public async Task<GetTicketDto> GetTicketByIdAsync(Guid id)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>()
            .GetByIdWithIncludesAsync(id, query => query
                .Include(t => t.Responses));
        if (ticket == null)
            throw new Exception("Ticket no encontrado");
        return mapper.Map<GetTicketDto>(ticket);
    }

    public async Task<IEnumerable<GetTicketDto>> GetAllTicketsAsync()
    {
        var tickets = await unitOfWork.GenericRepository<Ticket>()
            .GetAllWithIncludesAsync(query => query
                .Include(t => t.Responses));
        return mapper.Map<IEnumerable<GetTicketDto>>(tickets);
    }

    public async Task UpdateTicketStatusAsync(Guid ticketId, UpdateTicketStatusDto updateTicketStatusDto)
    {
        var ticket = await unitOfWork.GenericRepository<Ticket>().GetByIdAsync(ticketId);
        if (ticket == null)
            throw new Exception("Ticket no encontrado");

        if (!Enum.TryParse<TicketStatus>(updateTicketStatusDto.Status, true, out var status))
            throw new Exception($"Estado Invalido - {typeof(TicketStatus)}");

        ticket.Status = status;
        ticket.ClosedAt = updateTicketStatusDto.CloseTicket switch
        {
            true when status == TicketStatus.Cerrado => ticket.ClosedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            false when status != TicketStatus.Cerrado => null,
            _ => ticket.ClosedAt
        };

        unitOfWork.GenericRepository<Ticket>().UpdateAsync(ticket);
        await unitOfWork.CompleteAsync();
    }
}