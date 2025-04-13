using Server.Data;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IAddressRepository Addresses { get; }
    public ICategoryRepository Categories { get; }
    public ICustomerRepository Customers { get; }
    public IOrderDetailRepository OrderDetails { get; }
    public IOrderRepository Orders { get; }
    public IPaymentRepository Payments { get; }
    public IProductRepository Products { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        Addresses = new AddressRepository(_context);
        Categories = new CategoryRepository(_context);
        Customers = new CustomerRepository(_context);
        OrderDetails = new OrderDetailRepository(_context);
        Orders = new OrderRepository(_context);
        Payments = new PaymentRepository(_context);
        Products = new ProductRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}