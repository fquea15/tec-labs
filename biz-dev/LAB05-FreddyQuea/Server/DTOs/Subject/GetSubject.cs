using Server.DTOs.Course;

namespace Server.DTOs.Subject;

public class GetSubject : SubjectBase
{
    public int SubjectId { get; set; }
}

public class GetSubjectWithCourse : SubjectBase
{
    public int SubjectId { get; set; }
    public GetCourse Course { get; set; } = null!;
}