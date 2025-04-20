﻿using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }
}
