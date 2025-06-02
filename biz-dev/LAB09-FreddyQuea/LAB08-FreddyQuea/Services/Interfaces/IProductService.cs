using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.DTOs.Product;

namespace LAB08_FreddyQuea.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<GetProductsAbovePrice>> GetProductsByPriceAsync(decimal price);
    Task<GetProduct> GetMostExpensiveProductAsync();
    Task<decimal> GetAverageProductPriceAsync();
    Task<IEnumerable<GetProduct>> GetProductsWithoutDescriptionAsync();
    Task<IEnumerable<GetCustomerName>> GetCustomersByProductIdAsync(int productId);
}