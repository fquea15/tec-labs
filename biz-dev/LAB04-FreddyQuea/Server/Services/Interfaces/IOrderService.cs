using Server.DTOs.Order;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<GetOrder>> GetAllAsync();
    Task<GetOrder?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateOrder order);
    Task<ServiceResponse> UpdateAsync(int id, UpdateOrder order);
    Task<ServiceResponse> DeleteAsync(int id);
}