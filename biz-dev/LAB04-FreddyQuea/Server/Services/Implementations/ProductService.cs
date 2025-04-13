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
        return mapper.Map<IEnumerable<GetProduct>>(products);
    }

    public async Task<GetProduct> GetByIdAsync(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        return mapper.Map<GetProduct>(product);
    }

    public async Task<ServiceResponse> AddAsync(CreateProduct product)
    {
        var mappedData = mapper.Map<Product>(product);
        await unitOfWork.Products.AddAsync(mappedData);
        var saved = await unitOfWork.CompleteAsync();
        return saved > 0
            ? new ServiceResponse(true, "Producto creado correctamente.")
            : new ServiceResponse(false, "Error al crear el producto.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateProduct product)
    {
        var existing = await unitOfWork.Products.GetByIdAsync(id);

        mapper.Map(product, existing);
        await unitOfWork.Products.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Producto actualizado correctamente.")
            : new ServiceResponse(false, "Error al actualizar el producto.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Products.GetByIdAsync(id);

        await unitOfWork.Products.DeleteAsync(existing.ProductId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Producto eliminado correctamente.")
            : new ServiceResponse(false, "Error al eliminar el producto.");
    }
}