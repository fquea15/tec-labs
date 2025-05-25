using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.DTOs.OrderDetail;
using LAB08_FreddyQuea.Models;

namespace LAB08_FreddyQuea.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<GetOrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
    Task<int> GetTotalQuantityByOrderIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date);
    Task<IEnumerable<GetAllOrderDetail>> GetAllOrderDetailsAsync();
    Task<List<GetOrderWithDetails>> GetOrderWithDetailsAsync(); 
}