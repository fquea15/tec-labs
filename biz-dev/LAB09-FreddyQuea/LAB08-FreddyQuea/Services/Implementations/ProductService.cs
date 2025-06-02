using AutoMapper;
using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.DTOs.Product;
using LAB08_FreddyQuea.Models;
using LAB08_FreddyQuea.Repositories.Interfaces;
using LAB08_FreddyQuea.Services.Interfaces;

namespace LAB08_FreddyQuea.Services.Implementations;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<GetProductsAbovePrice>> GetProductsByPriceAsync(decimal price)
    {
        var products = await unitOfWork.ProductRepository.GetProductsByPriceAsync(price);
        return mapper.Map<IEnumerable<GetProductsAbovePrice>>(products);
    }
    
    public async Task<GetProduct> GetMostExpensiveProductAsync()
    {
        var product = await unitOfWork.ProductRepository.GetMostExpensiveProductAsync();
        return mapper.Map<GetProduct>(product);
    }
    
    public async Task<decimal> GetAverageProductPriceAsync()
    {
        return await unitOfWork.ProductRepository.GetAverageProductPriceAsync();
    }
    
    public async Task<IEnumerable<GetProduct>> GetProductsWithoutDescriptionAsync()
    {
        var products = await unitOfWork.ProductRepository.GetProductsWithoutDescriptionAsync();
        return mapper.Map<IEnumerable<GetProduct>>(products);
    }
    
    public async Task<IEnumerable<GetCustomerName>> GetCustomersByProductIdAsync(int productId)
    {
        return await unitOfWork.ProductRepository.GetCustomersByProductIdAsync(productId);
    }
}












