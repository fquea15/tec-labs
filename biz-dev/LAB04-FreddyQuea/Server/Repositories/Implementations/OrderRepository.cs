using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
}