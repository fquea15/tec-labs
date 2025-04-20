using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.DTOs.Course;
using Server.Models;
using Server.Services.Interfaces;
using Server.Repositories.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class CourseService(IUnitOfWork unitOfWork, IMapper mapper) : ICourseService
{
    public async Task<ServiceResponse<IEnumerable<GetCourse>>> GetAllAsync()
    {
        var courses = await unitOfWork.Repository<Course>()?.GetAllAsync()!;
        var result = mapper.Map<IEnumerable<GetCourse>>(courses);
        return new ServiceResponse<IEnumerable<GetCourse>>(true, "Lista de cursos obtenida", result);
    }

    public async Task<ServiceResponse<GetCourseWithSubjects>> GetByIdAsync(int id)
    {
        var dbSet = unitOfWork.Repository<Course>()?.AsQueryable();

        Debug.Assert(dbSet != null, nameof(dbSet) + " != null");
        var course = await dbSet
            .Include(c => c.Subjects)
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course == null)
            return new ServiceResponse<GetCourseWithSubjects>(false, "Curso no encontrado");

        var result = mapper.Map<GetCourseWithSubjects>(course);
        return new ServiceResponse<GetCourseWithSubjects>(true, "Curso encontrado", result);
    }


    public async Task<ServiceResponse<GetCourse>> CreateAsync(CreateCourse createCourse)
    {
        var course = mapper.Map<Course>(createCourse);
        await unitOfWork.Repository<Course>()?.AddAsync(course)!;
        return new ServiceResponse<GetCourse>(true, "Curso creado correctamente", mapper.Map<GetCourse>(course));
    }

    public async Task<ServiceResponse<GetCourse>> UpdateAsync(int id, UpdateCourse updateCourse)
    {
        var repo = unitOfWork.Repository<Course>();
        var course = await repo?.GetByIdAsync(id)!;

        if (course == null)
            return new ServiceResponse<GetCourse>(false, "Curso no encontrado");

        mapper.Map(updateCourse, course);
        repo.Update(course);
        await unitOfWork.CompleteAsync();

        return new ServiceResponse<GetCourse>(true, "Curso actualizado correctamente", mapper.Map<GetCourse>(course));
    }

    public async Task<ServiceResponse<string>> DeleteAsync(int id)
    {
        var repo = unitOfWork.Repository<Course>();
        var course = await repo?.GetByIdAsync(id)!;

        if (course == null)
            return new ServiceResponse<string>(false, "Curso no encontrado");

        repo.Delete(id);
        await unitOfWork.CompleteAsync();

        return new ServiceResponse<string>(true, "Curso eliminado correctamente");
    }
}