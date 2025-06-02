using Lab10.Application.DTOs.Role;
using Lab10.Application.DTOs.Ticket;

namespace Lab10.Application.DTOs.User;

public class GetUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GetRoleDto> Roles { get; set; } = new();
    public List<GetTicketDto> Tickets { get; set; } = new();
}