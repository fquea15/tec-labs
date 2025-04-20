using Server.DTOs.Subject;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface ISubjectService
{
    Task<ServiceResponse<IEnumerable<GetSubjectWithCourse>>> GetAllAsync();
    Task<ServiceResponse<GetSubjectWithCourse>> GetByIdAsync(int id);
    Task<ServiceResponse<GetSubjectWithCourse>> CreateAsync(CreateSubject createSubject);
    Task<ServiceResponse<GetSubjectWithCourse>> UpdateAsync(int id, UpdateSubject updateSubject);
    Task<ServiceResponse<bool>> DeleteAsync(int id);
}