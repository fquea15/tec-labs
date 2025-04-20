using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Credits { get; set; }

    public int? TeacherId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public virtual Teacher? Teacher { get; set; }
}
