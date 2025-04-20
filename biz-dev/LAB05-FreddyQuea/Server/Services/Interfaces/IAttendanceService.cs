using Server.DTOs.Attendance;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IAttendanceService
{
    Task<ServiceResponse<IEnumerable<GetAttendance>>> GetByStudentAsync(int studentId);
    Task<ServiceResponse<IEnumerable<GetAttendance>>> GetByCourseAsync(int courseId);
    Task<ServiceResponse<GetAttendance>> CreateAsync(CreateAttendance createAttendance);
}