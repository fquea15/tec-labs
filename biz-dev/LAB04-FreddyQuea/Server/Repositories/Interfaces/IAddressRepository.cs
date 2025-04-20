using Server.Models;

namespace Server.Repositories.Interfaces;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address?> GetAddressByCustomerIdAsync(int customerId);
    Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId);
}