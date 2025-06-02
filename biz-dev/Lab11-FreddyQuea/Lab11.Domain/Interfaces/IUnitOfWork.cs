namespace Lab11.Domain.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();
}