namespace Lab11.Application.DTOs.Ticket;

public class UpdateTicketStatusDto
{
    public string Status { get; set; }
    public bool CloseTicket { get; set; } = true;
}