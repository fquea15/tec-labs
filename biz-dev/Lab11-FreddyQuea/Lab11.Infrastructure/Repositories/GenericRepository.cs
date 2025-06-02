using System.Linq.Expressions;
using Lab11.Domain.Interfaces;
using Lab11.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
    {
        IQueryable<TEntity> query = _dbSet;
        query = include(query);
        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdWithIncludesAsync(Guid id,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
    {
        IQueryable<TEntity> query = _dbSet;
        query = include(query);

        var entityName = typeof(TEntity).Name;
        var keyProperty = entityName + "Id";

        return await query.FirstOrDefaultAsync(e =>
            EF.Property<Guid>(e, keyProperty) == id) ?? throw new InvalidOperationException();
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }


    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }
}