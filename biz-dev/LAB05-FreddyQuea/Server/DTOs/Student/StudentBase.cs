namespace Server.DTOs.Student;

public class StudentBase
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}