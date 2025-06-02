using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.DTOs.OrderDetail;

namespace LAB08_FreddyQuea.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<GetOrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
    Task<int> GetTotalQuantityByOrderIdAsync(int orderId);
    Task<IEnumerable<GetOrder>> GetOrdersAfterDateAsync(DateTime date);
    Task<IEnumerable<GetAllOrderDetail>> GetAllOrderDetailsAsync();
    Task<IEnumerable<GetOrderWithDetails>> GetOrderWithDetailsAsync();
}