using Server.DTOs.Category;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategory>> GetAllAsync();
    Task<GetCategory?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateCategory category);
    Task<ServiceResponse> UpdateAsync(int id, UpdateCategory category);
    Task<ServiceResponse> DeleteAsync(int id);
}