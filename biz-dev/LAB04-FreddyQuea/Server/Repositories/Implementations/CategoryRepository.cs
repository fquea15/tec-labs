using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class CategoryRepository(ApplicationDbContext context)
    : GenericRepository<Category>(context), ICategoryRepository
{
}