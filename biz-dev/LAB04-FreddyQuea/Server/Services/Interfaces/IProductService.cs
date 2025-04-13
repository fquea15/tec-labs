using Server.DTOs.Product;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<GetProduct>> GetAllAsync();
    Task<GetProduct> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateProduct product);
    Task<ServiceResponse> UpdateAsync(int id, UpdateProduct product);
    Task<ServiceResponse> DeleteAsync(int id);
}