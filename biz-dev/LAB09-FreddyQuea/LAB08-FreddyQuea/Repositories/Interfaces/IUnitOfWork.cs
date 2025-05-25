namespace LAB08_FreddyQuea.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : class;
    ICustumerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    Task<int> CompleteAsync();
}