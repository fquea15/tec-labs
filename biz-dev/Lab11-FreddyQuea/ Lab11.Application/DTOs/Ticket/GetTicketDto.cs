using Lab11.Application.DTOs.Response;

namespace Lab11.Application.DTOs.Ticket;

public class GetTicketDto
{
    public Guid TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public string Status { get; set; }

    public Guid UserId { get; set; }
    public List<GetResponseDto> Responses { get; set; }
}