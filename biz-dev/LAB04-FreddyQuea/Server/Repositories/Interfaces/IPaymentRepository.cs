using Server.Models;

namespace Server.Repositories.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId);
}