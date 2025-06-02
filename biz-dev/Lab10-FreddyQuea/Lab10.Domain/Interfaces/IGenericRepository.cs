using System.Linq.Expressions;

namespace Lab10.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdWithIncludesAsync(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    Task AddAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}