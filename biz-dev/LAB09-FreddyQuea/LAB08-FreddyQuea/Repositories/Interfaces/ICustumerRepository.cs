using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.Models;

namespace LAB08_FreddyQuea.Repositories.Interfaces;

public interface ICustumerRepository : IGenericRepository<Client>
{
    Task<CustomerOrderCount?> GetCustomerWithMostOrdersAsync();
    Task<IEnumerable<GetCustomerProduct>> GetProductsSoldToCustomerAsync(int customerId);
    Task<IEnumerable<GetCustomerOrders>> GetCustomersWithOrdersAsync();
    Task<IEnumerable<GetCustomerProductCount>> GetCustomersWithProductCountAsync(); 
    Task<IEnumerable<GetCustomerSales>> GetCustomerSalesAsync();

}