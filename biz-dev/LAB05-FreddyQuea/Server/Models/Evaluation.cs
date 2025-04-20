using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public decimal? Grade { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }
}
