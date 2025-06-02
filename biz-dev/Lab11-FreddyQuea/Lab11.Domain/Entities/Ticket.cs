using Lab11.Domain.Enums;

namespace Lab11.Domain.Entities;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Abierto;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual User User { get; set; } = null!;
}
