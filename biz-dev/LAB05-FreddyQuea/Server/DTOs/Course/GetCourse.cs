using Server.DTOs.Subject;

namespace Server.DTOs.Course;

public class GetCourse : CourseBase
{
    public int CourseId { get; set; }
}

public class GetCourseWithSubjects : GetCourse
{
    public List<GetSubject> Subjects { get; set; } = [];
}