using System;
using System.Collections.Generic;
using Lab10.Domain.Enum;

namespace Lab10.Domain.Entities;

public partial class Ticket
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Abierto;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual User User { get; set; } = null!;
}
