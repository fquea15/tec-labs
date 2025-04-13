using Server.DTOs.OrderDetail;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IOrderDetailService
{
    Task<IEnumerable<GetOrderDetail>> GetAllAsync();
    Task<GetOrderDetail?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateOrderDetail orderDetail);
    Task<ServiceResponse> UpdateAsync(int id, UpdateOrderDetail orderDetail);
    Task<ServiceResponse> DeleteAsync(int id);
}