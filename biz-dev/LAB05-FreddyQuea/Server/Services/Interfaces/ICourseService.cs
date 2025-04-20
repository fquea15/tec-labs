using Server.DTOs.Course;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface ICourseService
{
    Task<ServiceResponse<IEnumerable<GetCourse>>> GetAllAsync();
    Task<ServiceResponse<GetCourseWithSubjects>> GetByIdAsync(int id);
    Task<ServiceResponse<GetCourse>> CreateAsync(CreateCourse createCourse);
    Task<ServiceResponse<GetCourse>> UpdateAsync(int id, UpdateCourse updateCourse);
    Task<ServiceResponse<string>> DeleteAsync(int id);
}