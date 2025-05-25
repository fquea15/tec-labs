namespace Lab10.Application.DTOs.Ticket;

public class UpdateTicketStatusDto
{
    public string Status { get; set; } = "Abierto";
    public bool CloseTicket { get; set; } = false;
}