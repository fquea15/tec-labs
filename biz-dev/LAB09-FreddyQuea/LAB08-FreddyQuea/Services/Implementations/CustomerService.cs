using AutoMapper;
using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.Models;
using LAB08_FreddyQuea.Repositories.Interfaces;
using LAB08_FreddyQuea.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB08_FreddyQuea.Services.Implementations;

public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
{
    public async Task<IEnumerable<GetCustomerByNameAll>> GetCustomersByNameAsync(string name)
    {
        var customers = await unitOfWork.Repository<Client>()
            ?.AsQueryable()
            .Where(c => c.Name.Contains(name))
            .ToListAsync()!;
        return mapper.Map<IEnumerable<GetCustomerByNameAll>>(customers);
    }

    public async Task<CustomerOrderCount?> GetCustomerWithMostOrdersAsync()
    {
        return await unitOfWork.CustomerRepository.GetCustomerWithMostOrdersAsync();
    }

    public async Task<IEnumerable<GetCustomerProduct>> GetProductsSoldToCustomerAsync(int customerId)
    {
        return await unitOfWork.CustomerRepository.GetProductsSoldToCustomerAsync(customerId);
    }

    public async Task<IEnumerable<GetCustomerOrders>> GetCustomersWithOrdersAsync()
    {
        return await unitOfWork.CustomerRepository.GetCustomersWithOrdersAsync();
    }

    public async Task<IEnumerable<GetCustomerProductCount>> GetCustomersWithProductCountAsync()
    {
        return await unitOfWork.CustomerRepository.GetCustomersWithProductCountAsync();
    }

    public async Task<IEnumerable<GetCustomerSales>> GetCustomerSalesAsync()
    {
        return await unitOfWork.CustomerRepository.GetCustomerSalesAsync();
    }
}