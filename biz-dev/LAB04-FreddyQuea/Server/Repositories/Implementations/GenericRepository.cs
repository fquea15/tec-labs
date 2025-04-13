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

    public async Task<int> AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        if (entity is null) return 0;

        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync();
    }
}