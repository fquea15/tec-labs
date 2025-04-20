using Server.DTOs.Student;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IStudentService
{
    Task<ServiceResponse<IEnumerable<GetStudent>>> GetAllAsync();
    Task<ServiceResponse<GetStudent>> GetByIdAsync(int id);
    Task<ServiceResponse<GetStudent>> CreateAsync(CreateStudent createStudent);
    Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateStudent updateStudent);
    Task<ServiceResponse<bool>> DeleteAsync(int id);
}
