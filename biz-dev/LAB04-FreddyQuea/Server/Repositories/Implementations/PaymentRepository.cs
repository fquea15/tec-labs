using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class PaymentRepository(ApplicationDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId)
    {
        return await _context.Payment.Where(p => p.OrderId == orderId)
            .ToListAsync();
    }
}