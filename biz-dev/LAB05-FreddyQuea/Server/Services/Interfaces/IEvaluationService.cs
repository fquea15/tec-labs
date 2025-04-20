using Server.DTOs.Evaluation;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IEvaluationService
{
    Task<ServiceResponse<IEnumerable<GetEvaluation>>> GetByStudentAsync(int studentId);
    Task<ServiceResponse<IEnumerable<GetEvaluation>>> GetByCourseAsync(int courseId);
    Task<ServiceResponse<GetEvaluation>> CreateAsync(CreateEvaluation createEvaluation);
}