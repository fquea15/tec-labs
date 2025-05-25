using LAB08_FreddyQuea.Data;
using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.Models;
using LAB08_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB08_FreddyQuea.Repositories.Implementations;

public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal price)
    {
        return await AsQueryable()
            .Where(p => p.Price > price)
            .ToListAsync();
    }

    public async Task<Product?> GetMostExpensiveProductAsync()
    {
        return await AsQueryable()
            .OrderByDescending(p => p.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetAverageProductPriceAsync()
    {
        return await AsQueryable()
            .Select(p => p.Price)
            .AverageAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync()
    {
        return await AsQueryable()
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();
    }

    public async Task<IEnumerable<GetCustomerName>> GetCustomersByProductIdAsync(int productId)
    {
        return await context.Orderdetails
            .Where(od => od.Productid == productId)
            .Join(context.Orders,
                od => od.Orderid,
                o => o.Orderid,
                (od, o) => new { o.Clientid })
            .Join(context.Clients,
                o => o.Clientid,
                c => c.Clientid,
                (o, c) => new GetCustomerName()
                {
                    CustomerName = c.Name
                })
            .Distinct()
            .ToListAsync();
    }
}