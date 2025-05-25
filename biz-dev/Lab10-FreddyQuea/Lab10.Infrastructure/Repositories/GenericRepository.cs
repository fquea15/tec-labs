using System.Linq.Expressions;
using Lab10.Domain.Interfaces;
using Lab10.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id) ?? throw new Exception($"{typeof(TEntity).Name} not found");
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdWithIncludesAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (include != null)
        {
            query = include(query);
        }
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id || EF.Property<Guid>(e, "Id") == id)
               ?? throw new Exception($"{typeof(TEntity).Name} not found");
    }

    public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (include != null)
        {
            query = include(query);
        }
        return await query.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
    }
}