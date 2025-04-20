using AutoMapper;
using Server.DTOs.Course;
using Server.DTOs.Enrollment;
using Server.DTOs.Student;
using Server.DTOs.Subject;
using Server.DTOs.Teacher;
using Server.Models;

namespace Server.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Student
        CreateMap<Student, GetStudent>();
        CreateMap<CreateStudent, Student>();
        CreateMap<UpdateStudent, Student>();

        // Teacher
        CreateMap<Teacher, GetTeacher>();
        CreateMap<CreateTeacher, Teacher>();
        CreateMap<UpdateTeacher, Teacher>();

        // Course
        CreateMap<Course, GetCourse>();
        CreateMap<CreateCourse, Course>();
        CreateMap<UpdateCourse, Course>();
        CreateMap<Course, GetCourseWithSubjects>();
        
        // Subject
        CreateMap<Subject, GetSubject>();
        CreateMap<Subject, GetSubjectWithCourse>();
        CreateMap<CreateSubject, Subject>();
        CreateMap<UpdateSubject, Subject>();
        
        // Enrollment
        CreateMap<Enrollment, GetEnrollment>();
        CreateMap<CreateEnrollment, Enrollment>();
        CreateMap<UpdateEnrollment, Enrollment>();

    }
}