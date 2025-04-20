using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class GenericRepository<TEntity>(ApplicationDbContext context)
    : IGenericRepository<TEntity> where TEntity : class
{
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var result = await context.Set<TEntity>().FindAsync(id);
        return result;
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Delete(int id)
    {
        var entity = context.Set<TEntity>().Find(id);
        if (entity != null)
        {
            context.Set<TEntity>().Remove(entity);
        }
    }
    
    public IQueryable<TEntity> AsQueryable()
    {
        return context.Set<TEntity>();
    }

}