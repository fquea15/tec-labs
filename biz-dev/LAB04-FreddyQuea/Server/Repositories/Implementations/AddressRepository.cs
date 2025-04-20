using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class AddressRepository(ApplicationDbContext context) : GenericRepository<Address>(context), IAddressRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Address?> GetAddressByCustomerIdAsync(int customerId)
    {
        return await _context.Set<Address>()
            .FirstOrDefaultAsync(a => a.CustomerId == customerId);
    }

    public async Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId)
    {
        return await _context.Set<Address>()
            .Where(a => a.CustomerId == customerId)
            .ToListAsync();
    }
}