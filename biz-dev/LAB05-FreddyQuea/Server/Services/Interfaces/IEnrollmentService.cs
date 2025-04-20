using Server.DTOs.Enrollment;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IEnrollmentService
{
    Task<ServiceResponse<IEnumerable<GetEnrollment>>> GetAllAsync();
    Task<ServiceResponse<GetEnrollment>> GetByIdAsync(int id);
    Task<ServiceResponse<GetEnrollment>> CreateAsync(CreateEnrollment createEnrollment);
    Task<ServiceResponse<GetEnrollment>> UpdateAsync(int id, UpdateEnrollment updateEnrollment);
    Task<ServiceResponse<bool>> DeleteAsync(int id);
}