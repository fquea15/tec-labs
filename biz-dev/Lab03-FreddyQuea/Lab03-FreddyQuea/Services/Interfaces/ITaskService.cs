using Lab03_FreddyQuea.DTOs;
using Lab03_FreddyQuea.DTOs.Task;

namespace Lab03_FreddyQuea.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<GetTask>> GetAllAsync();
    Task<GetTask> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateTask task);
    Task<ServiceResponse> UpdateAsync(UpdateTask task);
    Task<ServiceResponse> DeleteAsync(int id);
}