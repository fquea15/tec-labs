using Server.DTOs.Enrollment;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using AutoMapper;
using Server.Utils;

namespace Server.Services.Implementations;

public class EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper) : IEnrollmentService
{
    public async Task<ServiceResponse<IEnumerable<GetEnrollment>>> GetAllAsync()
    {
        var enrollments = await unitOfWork.Repository<Enrollment>()?.GetAllAsync()!;
        var result = mapper.Map<IEnumerable<GetEnrollment>>(enrollments);
        return new ServiceResponse<IEnumerable<GetEnrollment>>(true, "Enrollments retrieved", result);
    }

    public async Task<ServiceResponse<GetEnrollment>> GetByIdAsync(int id)
    {
        var enrollment = await unitOfWork.Repository<Enrollment>()?.GetByIdAsync(id)!;
        if (enrollment == null)
            return new ServiceResponse<GetEnrollment>(false, "Enrollment not found");

        var result = mapper.Map<GetEnrollment>(enrollment);
        return new ServiceResponse<GetEnrollment>(true, "Enrollment found", result);
    }

    public async Task<ServiceResponse<GetEnrollment>> CreateAsync(CreateEnrollment createEnrollment)
    {
        var enrollment = mapper.Map<Enrollment>(createEnrollment);
        await unitOfWork.Repository<Enrollment>()?.AddAsync(enrollment)!;
        await unitOfWork.CompleteAsync();

        var result = mapper.Map<GetEnrollment>(enrollment);
        return new ServiceResponse<GetEnrollment>(true, "Enrollment created", result);
    }

    public async Task<ServiceResponse<GetEnrollment>> UpdateAsync(int id, UpdateEnrollment updateEnrollment)
    {
        var enrollment = await unitOfWork.Repository<Enrollment>()?.GetByIdAsync(id)!;
        if (enrollment == null)
            return new ServiceResponse<GetEnrollment>(false, "Enrollment not found");

        mapper.Map(updateEnrollment, enrollment);
        unitOfWork.Repository<Enrollment>()?.Update(enrollment);
        await unitOfWork.CompleteAsync();

        var result = mapper.Map<GetEnrollment>(enrollment);
        return new ServiceResponse<GetEnrollment>(true, "Enrollment updated", result);
    }

    public async Task<ServiceResponse<bool>> DeleteAsync(int id)
    {
        var enrollment = await unitOfWork.Repository<Enrollment>()?.GetByIdAsync(id)!;
        if (enrollment == null)
            return new ServiceResponse<bool>(false, "Enrollment not found", false);

        unitOfWork.Repository<Enrollment>()?.Delete(id);
        await unitOfWork.CompleteAsync();
        return new ServiceResponse<bool>(true, "Enrollment deleted", true);
    }
}