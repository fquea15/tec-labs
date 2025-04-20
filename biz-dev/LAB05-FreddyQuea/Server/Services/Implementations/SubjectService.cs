using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.DTOs.Subject;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class SubjectService(IUnitOfWork unitOfWork, IMapper mapper) : ISubjectService
{
    public async Task<ServiceResponse<IEnumerable<GetSubjectWithCourse>>> GetAllAsync()
    {
        var dbSet = unitOfWork.Repository<Subject>()?.AsQueryable();

        Debug.Assert(dbSet != null, nameof(dbSet) + " != null");
        var subjects = await dbSet
            .Include(s => s.Course)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<GetSubjectWithCourse>>(subjects);
        return new ServiceResponse<IEnumerable<GetSubjectWithCourse>>(true, "Materias encontradas", result);
    }

    public async Task<ServiceResponse<GetSubjectWithCourse>> GetByIdAsync(int id)
    {
        var dbSet = unitOfWork.Repository<Subject>()?.AsQueryable();

        Debug.Assert(dbSet != null, nameof(dbSet) + " != null");
        var subject = await dbSet
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.SubjectId == id);

        if (subject == null)
            return new ServiceResponse<GetSubjectWithCourse>(false, "Materia no encontrada");

        var result = mapper.Map<GetSubjectWithCourse>(subject);
        return new ServiceResponse<GetSubjectWithCourse>(true, "Materia encontrada", result);
    }

    public async Task<ServiceResponse<GetSubjectWithCourse>> CreateAsync(CreateSubject dto)
    {
        var subject = mapper.Map<Subject>(dto);

        await unitOfWork.Repository<Subject>()?.AddAsync(subject)!;
        await unitOfWork.CompleteAsync();

        var result = mapper.Map<GetSubjectWithCourse>(subject);
        return new ServiceResponse<GetSubjectWithCourse>(true, "Materia creada correctamente", result);
    }

    public async Task<ServiceResponse<GetSubjectWithCourse>> UpdateAsync(int id, UpdateSubject updateSubject)
    {
        var repo = unitOfWork.Repository<Subject>();
        Debug.Assert(repo != null, nameof(repo) + " != null");
        var subject = await repo.GetByIdAsync(id);

        if (subject == null)
            return new ServiceResponse<GetSubjectWithCourse>(false, "Materia no encontrada");

        mapper.Map(updateSubject, subject);
        repo.Update(subject);
        await unitOfWork.CompleteAsync();

        var result = mapper.Map<GetSubjectWithCourse>(subject);
        return new ServiceResponse<GetSubjectWithCourse>(true, "Materia actualizada correctamente", result);
    }

    public async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        var repo = unitOfWork.Repository<Subject>();
        var subject = await repo?.GetByIdAsync(id)!;

        if (subject == null)
            return new ServiceResponse<bool>(false, "Materia no encontrada");

        repo.Delete(id);
        await unitOfWork.CompleteAsync();

        return new ServiceResponse<bool>(true, "Materia eliminada correctamente", true);
    }
}