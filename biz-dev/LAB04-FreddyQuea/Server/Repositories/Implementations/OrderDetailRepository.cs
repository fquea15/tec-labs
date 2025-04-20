using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class OrderDetailRepository(ApplicationDbContext context)
    : GenericRepository<OrderDetail>(context), IOrderDetailRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderDetail
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }
}