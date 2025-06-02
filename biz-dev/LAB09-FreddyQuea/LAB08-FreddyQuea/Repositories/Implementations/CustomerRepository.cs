using LAB08_FreddyQuea.Data;
using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.Models;
using LAB08_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB08_FreddyQuea.Repositories.Implementations;

public class CustomerRepository(ApplicationDbContext context) : GenericRepository<Client>(context), ICustumerRepository
{
    public async Task<CustomerOrderCount?> GetCustomerWithMostOrdersAsync()
    {
        return await context.Orders
            .GroupBy(o => o.Clientid)
            .Select(g => new
            {
                CustomerId = g.Key,
                OrderCount = g.Count(),
                CustomerName = g.First().Client.Name
            })
            .OrderByDescending(x => x.OrderCount)
            .Select(x => new CustomerOrderCount
            {
                CustomerId = x.CustomerId,
                Name = x.CustomerName,
                OrderCount = x.OrderCount
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GetCustomerProduct>> GetProductsSoldToCustomerAsync(int customerId)
    {
        return await context.Orders
            .Where(o => o.Clientid == customerId)
            .Join(context.Orderdetails,
                o => o.Orderid,
                od => od.Orderid,
                (o, od) => new { od.Productid })
            .Join(context.Products,
                od => od.Productid,
                p => p.Productid,
                (od, p) => new GetCustomerProduct()
                {
                    ProductName = p.Name
                })
            .Distinct()
            .ToListAsync();
    }

    // Get Customers with orders
    public async Task<IEnumerable<GetCustomerOrders>> GetCustomersWithOrdersAsync()
    {
        return await context.Clients
            .Include(c => c.Orders) // SOLUCIÓN N+1
            .AsNoTracking()
            .Where(c => c.Orders.Any()) // Solo clientes con pedidos
            .Select(customer => new GetCustomerOrders
            {
                CustomerName = customer.Name,
                Orders = customer.Orders
                    .OrderByDescending(o => o.Orderdate) // Orden lógico
                    .Select(order => new GetOrder
                    {
                        Orderid = order.Orderid,
                        Clientid = order.Clientid,
                        Orderdate = order.Orderdate,
                    }).ToList()
            }).ToListAsync();
    }

    public async Task<IEnumerable<GetCustomerProductCount>> GetCustomersWithProductCountAsync()
    {
        return await context.Clients
            .AsNoTracking()
            .Select(client => new GetCustomerProductCount
            {
                CustomerName = client.Name,
                TotalProducts = client.Orders.Sum(order => order.Orderdetails.Sum(detail => detail.Quantity))
            }).ToListAsync();
    }

    public async Task<IEnumerable<GetCustomerSales>> GetCustomerSalesAsync()
    {
        return await context.Orders
            .Join(context.Clients,
                order => order.Clientid,
                client => client.Clientid,
                (order, client) => new { order, client.Name })
            .Join(context.Orderdetails,
                oc => oc.order.Orderid,
                detail => detail.Orderid,
                (oc, detail) => new { oc.order, oc.Name, detail })
            .Join(context.Products,
                ocd => ocd.detail.Productid,
                product => product.Productid,
                (ocd, product) => new
                {
                    Clientid = ocd.order.Clientid,
                    CustomerName = ocd.Name,
                    Sales = ocd.detail.Quantity * product.Price
                })
            .GroupBy(x => new { x.Clientid, x.CustomerName })
            .Select(group => new GetCustomerSales
            {
                CustomerName = group.Key.CustomerName,
                TotalSales = group.Sum(x => x.Sales)
            })
            .OrderByDescending(s => s.TotalSales)
            .AsNoTracking()
            .ToListAsync();
    }
}