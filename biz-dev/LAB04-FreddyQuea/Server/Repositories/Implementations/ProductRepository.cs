using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
{
}