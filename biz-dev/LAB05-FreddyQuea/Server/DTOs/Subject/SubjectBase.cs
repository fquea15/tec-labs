namespace Server.DTOs.Subject;

public class SubjectBase
{
    public int? CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}