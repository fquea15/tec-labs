using Lab10.Application.DTOs.Ticket;

namespace Lab10.Application.Interfaces;

public interface ITicketService
{
    Task<GetTicketDto> CreateTicketAsync(CreateTicketDto createTicketDto, Guid userId);
    Task<GetTicketDto> GetTicketByIdAsync(Guid id);
    Task<IEnumerable<GetTicketDto>> GetAllTicketsAsync();
    Task UpdateTicketStatusAsync(Guid ticketId, UpdateTicketStatusDto updateTicketStatusDto);
}