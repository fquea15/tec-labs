namespace Server.DTOs.Teacher;

public class TeacherBase
{
    public string Name { get; set; } = null!;
    public string? Specialty { get; set; }
    public string? Email { get; set; }
}