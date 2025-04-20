namespace Server.DTOs.Attendance;

public class AttendanceBase
{
    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public DateOnly? Date { get; set; }
    public string? Status { get; set; }
}