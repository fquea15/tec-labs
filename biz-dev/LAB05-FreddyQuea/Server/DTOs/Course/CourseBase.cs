namespace Server.DTOs.Course;

public class CourseBase
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Credits { get; set; }
    public int? TeacherId { get; set; }
}