namespace Server.DTOs.Enrollment;

public class EnrollmentBase
{
    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public string? Semester { get; set; }
}