using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.Models;

namespace LAB08_FreddyQuea.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal price);
    Task<Product?> GetMostExpensiveProductAsync();
    Task<decimal> GetAverageProductPriceAsync();
    Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync();
    Task<IEnumerable<GetCustomerName>> GetCustomersByProductIdAsync(int productId);
}