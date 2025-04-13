namespace Server.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAddressRepository Addresses { get; }
    ICategoryRepository Categories { get; }
    ICustomerRepository Customers { get; }
    IOrderDetailRepository OrderDetails { get; }
    IOrderRepository Orders { get; }
    IPaymentRepository Payments { get; }
    IProductRepository Products { get; }
    Task<int> CompleteAsync();
}