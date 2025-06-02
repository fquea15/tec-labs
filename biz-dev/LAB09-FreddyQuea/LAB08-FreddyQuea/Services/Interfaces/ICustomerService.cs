using LAB08_FreddyQuea.DTOs.Customer;

namespace LAB08_FreddyQuea.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<GetCustomerByNameAll>> GetCustomersByNameAsync(string name);
    Task<CustomerOrderCount?> GetCustomerWithMostOrdersAsync();
    Task<IEnumerable<GetCustomerProduct>> GetProductsSoldToCustomerAsync(int customerId);
    Task<IEnumerable<GetCustomerOrders>> GetCustomersWithOrdersAsync();
    Task<IEnumerable<GetCustomerProductCount>> GetCustomersWithProductCountAsync();
    Task<IEnumerable<GetCustomerSales>> GetCustomerSalesAsync(); 
}