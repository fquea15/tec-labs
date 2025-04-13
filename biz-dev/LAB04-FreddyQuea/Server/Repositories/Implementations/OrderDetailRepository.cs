using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class OrderDetailRepository(ApplicationDbContext context)
    : GenericRepository<OrderDetail>(context), IOrderDetailRepository
{
}