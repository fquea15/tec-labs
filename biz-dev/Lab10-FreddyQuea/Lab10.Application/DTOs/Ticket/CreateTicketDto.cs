namespace Lab10.Application.DTOs.Ticket;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}