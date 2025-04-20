using AutoMapper;
using Server.DTOs.Student;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class StudentService(IGenericRepository<Student> repository, IMapper mapper) : IStudentService
{
    public async Task<ServiceResponse<IEnumerable<GetStudent>>> GetAllAsync()
    {
        var students = await repository.GetAllAsync();
        var result = mapper.Map<IEnumerable<GetStudent>>(students);
        return new ServiceResponse<IEnumerable<GetStudent>>(true, "Students retrieved successfully", result);
    }

    public async Task<ServiceResponse<GetStudent>> GetByIdAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student == null)
            return new ServiceResponse<GetStudent>(false, "Student not found");

        var getStudent = mapper.Map<GetStudent>(student);
        return new ServiceResponse<GetStudent>(true, "Student retrieved successfully", getStudent);
    }

    public async Task<ServiceResponse<GetStudent>> CreateAsync(CreateStudent createStudent)
    {
        var student = mapper.Map<Student>(createStudent);
        await repository.AddAsync(student);
        var created = mapper.Map<GetStudent>(student);

        return new ServiceResponse<GetStudent>(true, "Student created successfully", created);
    }

    public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateStudent updateStudent)
    {
        var student = await repository.GetByIdAsync(id);
        if (student == null)
            return new ServiceResponse<bool>(false, "Student not found");

        mapper.Map(updateStudent, student);
        repository.Update(student);

        return new ServiceResponse<bool>(true, "Student updated successfully", true);
    }

    public async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        var student = await repository.GetByIdAsync(id);
        if (student == null)
            return new ServiceResponse<bool>(false, "Student not found");

        repository.Delete(student.StudentId);
        return new ServiceResponse<bool>(true, "Student deleted successfully", true);
    }
}
