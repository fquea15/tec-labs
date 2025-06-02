using System.Linq.Expressions;

namespace Lab11.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
    Task<TEntity?> GetByIdWithIncludesAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}