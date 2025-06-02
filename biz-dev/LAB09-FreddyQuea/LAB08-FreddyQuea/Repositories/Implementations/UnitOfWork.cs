using System.Collections;
using LAB08_FreddyQuea.Data;
using LAB08_FreddyQuea.Repositories.Interfaces;

namespace LAB08_FreddyQuea.Repositories.Implementations;

public class UnitOfWork(
    ApplicationDbContext context,
    IProductRepository productRepository,
    ICustumerRepository custumerRepository,
    IOrderRepository orderRepository) : IUnitOfWork
{
    private readonly Hashtable _repositories = new();
    private IProductRepository _productRepository = productRepository;
    private IOrderRepository _orderRepository = orderRepository;
    private ICustumerRepository _customerRepository = custumerRepository;

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var typeName = typeof(TEntity).Name;

        if (_repositories.ContainsKey(typeName))
        {
            return (IGenericRepository<TEntity>)_repositories[typeName]!;
        }

        var repositoryInstance = new GenericRepository<TEntity>(context);
        _repositories[typeName] = repositoryInstance;

        return repositoryInstance;
    }

    public IProductRepository ProductRepository
    {
        get => _productRepository ??= new ProductRepository(context);
    }

    public IOrderRepository OrderRepository
    {
        get => _orderRepository ??= new OrderRepository(context);
    }
    
    public ICustumerRepository CustomerRepository
    {
        get => _customerRepository ??= new CustomerRepository(context);
    }

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}