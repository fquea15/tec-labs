using Lab10.Application.DTOs.Response;

namespace Lab10.Application.DTOs.Ticket;

public class GetTicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "Abierto";
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public List<GetResponseDto> Responses { get; set; } = new();
}