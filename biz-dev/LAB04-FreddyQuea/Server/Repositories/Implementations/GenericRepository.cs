using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var result = await context.Set<T>().FindAsync(id);
        return result;
    }

    public void AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void DeleteAsync(int id)
    {
        var entity = context.Set<T>().Find(id);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
        }
    }
}