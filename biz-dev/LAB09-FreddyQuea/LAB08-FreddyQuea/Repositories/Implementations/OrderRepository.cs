using LAB08_FreddyQuea.Data;
using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.DTOs.OrderDetail;
using LAB08_FreddyQuea.Models;
using LAB08_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB08_FreddyQuea.Repositories.Implementations;

public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public async Task<IEnumerable<GetOrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        return await context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => new GetOrderDetail()
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
    {
        return await context.Orderdetails
            .Where(od => od.Orderid == orderId)
            .Select(od => od.Quantity)
            .SumAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date)
    {
        return await AsQueryable()
            .Where(o => o.Orderdate > date)
            .ToListAsync();
    }

    public async Task<IEnumerable<GetAllOrderDetail>> GetAllOrderDetailsAsync()
    {
        return await context.Orderdetails
            .Select(od => new GetAllOrderDetail()
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }

    public async Task<List<GetOrderWithDetails>> GetOrderWithDetailsAsync()
    {
        return await context.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .Select(order => new GetOrderWithDetails
            {
                OrderId = order.Orderid,
                OrderDate = order.Orderdate,
                Products = order.Orderdetails.Select(od => new GetOrderDetail
                {
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    Price = od.Product.Price
                }).ToList()
            })
            .ToListAsync();
    }
}