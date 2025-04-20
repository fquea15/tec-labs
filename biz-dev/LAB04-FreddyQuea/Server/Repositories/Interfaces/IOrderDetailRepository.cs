using Server.Models;

namespace Server.Repositories.Interfaces;

public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId);
}