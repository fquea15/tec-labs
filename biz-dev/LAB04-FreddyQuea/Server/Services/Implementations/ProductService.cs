using AutoMapper;
using Server.DTOs.Product;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<GetProduct>> GetAllAsync()
    {
        var products = await unitOfWork.Products.GetAllAsync();
        var result = mapper.Map<List<GetProduct>>(products);

        foreach (var item in result)
        {
            var category = await unitOfWork.Categories.GetByIdAsync(item.CategoryId);
            if (category?.Name != null) item.CategoryName = category.Name;
        }

        return result;
    }

    public async Task<GetProduct> GetByIdAsync(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        return mapper.Map<GetProduct>(product);
    }

    public async Task<ServiceResponse> AddAsync(CreateProduct product)
    {
        var mappedData = mapper.Map<Product>(product);
        unitOfWork.Products.AddAsync(mappedData);
        var saved = await unitOfWork.CompleteAsync();
        return saved > 0
            ? new ServiceResponse(true, "Producto creado correctamente.")
            : new ServiceResponse(false, "Error al crear el producto.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateProduct product)
    {
        var existing = await unitOfWork.Products.GetByIdAsync(id);

        mapper.Map(product, existing);
        if (existing != null) unitOfWork.Products.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Producto actualizado correctamente.")
            : new ServiceResponse(false, "Error al actualizar el producto.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Products.GetByIdAsync(id);

        if (existing != null) unitOfWork.Products.DeleteAsync(existing.ProductId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Producto eliminado correctamente.")
            : new ServiceResponse(false, "Error al eliminar el producto.");
    }
}