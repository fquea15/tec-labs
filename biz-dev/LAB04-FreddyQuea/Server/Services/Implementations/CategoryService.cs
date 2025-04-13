using AutoMapper;
using Server.DTOs.Category;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<IEnumerable<GetCategory>> GetAllAsync()
    {
        var categories = await unitOfWork.Categories.GetAllAsync();
        return mapper.Map<IEnumerable<GetCategory>>(categories);
    }

    public async Task<GetCategory?> GetByIdAsync(int id)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        return mapper.Map<GetCategory>(category);
    }

    public async Task<ServiceResponse> AddAsync(CreateCategory category)
    {
        var entity = mapper.Map<Category>(category);
        await unitOfWork.Categories.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Categoría creada correctamente.")
            : new ServiceResponse(false, "No se pudo crear la categoría.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateCategory category)
    {
        var existing = await unitOfWork.Categories.GetByIdAsync(id);

        mapper.Map(category, existing);
        await unitOfWork.Categories.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Categoría actualizada correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar la categoría.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Categories.GetByIdAsync(id);

        await unitOfWork.Categories.DeleteAsync(existing.CategoryId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Categoría eliminada correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar la categoría.");
    }
}