using Lab03_FreddyQuea.Data;
using Lab03_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab03_FreddyQuea.Repositories.Implementations;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAll() => await context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetById(int id) => await context.Set<T>().FindAsync(id);

    public async Task<int> Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> Update(T entity)
    {
        context.Set<T>().Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        if (entity is null) return 0;

        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync();
    }
}

/*
 *
 * 

  public async Task<int> AddAsync(TEntity entity)
  {
    _context.Set<TEntity>().Add(entity);
    return await _context.SaveChangesAsync();
  }

  public async Task<int> UpdateAsync(TEntity entity)
  {
    _context.Set<TEntity>().Update(entity);
    return await _context.SaveChangesAsync();
  }

  public async Task<int> DeleteAsync(Guid id)
  {
    var entity = await _context.Set<TEntity>().FindAsync(id);
    if (entity is null) return 0;

    _context.Set<TEntity>().Remove(entity);
    return await _context.SaveChangesAsync();
  }
 */
