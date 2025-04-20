using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string? Specialty { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
