using Server.DTOs.Customer;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<GetCustomer>> GetAllAsync();
    Task<GetCustomer?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateCustomer customer);
    Task<ServiceResponse> UpdateAsync(int id, UpdateCustomer customer);
    Task<ServiceResponse> DeleteAsync(int id);
}