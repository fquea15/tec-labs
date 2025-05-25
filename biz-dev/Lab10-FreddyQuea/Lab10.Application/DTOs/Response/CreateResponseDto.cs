namespace Lab10.Application.DTOs.Response;

public class CreateResponseDto
{
    public Guid TicketId { get; set; }
    public string Message { get; set; } = string.Empty;
}