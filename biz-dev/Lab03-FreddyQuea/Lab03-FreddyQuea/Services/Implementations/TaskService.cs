using AutoMapper;
using Lab03_FreddyQuea.DTOs;
using Lab03_FreddyQuea.DTOs.Task;
using Lab03_FreddyQuea.Repositories.Interfaces;
using Lab03_FreddyQuea.Services.Interfaces;

namespace Lab03_FreddyQuea.Services.Implementations;

public class TaskService(IGenericRepository<Models.Task> generic, IMapper mapper) : ITaskService
{
    public async Task<IEnumerable<GetTask>> GetAllAsync()
    {
        var rawData = await generic.GetAll();
        return !rawData.Any() ? [] : mapper.Map<IEnumerable<GetTask>>(rawData);
    }

    public async Task<GetTask> GetByIdAsync(int id)
    {
        var rawData = await generic.GetById(id);
        return mapper.Map<GetTask>(rawData);
    }

    public async Task<ServiceResponse> AddAsync(CreateTask task)
    {
        var mappedData = mapper.Map<Models.Task>(task);
        var result = await generic.Add(mappedData);
        return result > 0
            ? new ServiceResponse(true, "Task Added!")
            : new ServiceResponse(false, "Task failed to be added!");
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateTask task)
    {
        var mappedData = mapper.Map<Models.Task>(task);
        var result = await generic.Update(mappedData);
        return result > 0
            ? new ServiceResponse(true, "Task Updated!")
            : new ServiceResponse(false, "Task failed to be updated!");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var result = await generic.Delete(id);

        return result > 0
            ? new ServiceResponse(true, "Task Deleted!")
            : new ServiceResponse(false, "Task not found or task to deleted.");
    }
}