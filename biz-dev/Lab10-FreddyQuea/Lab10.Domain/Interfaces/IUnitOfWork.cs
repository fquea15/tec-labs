namespace Lab10.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}