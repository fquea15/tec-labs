namespace Lab11.Application.DTOs.Response;

public class GetResponseDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}