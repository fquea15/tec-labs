using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories.Implementations;

public class AddressRepository(ApplicationDbContext context) : GenericRepository<Address>(context), IAddressRepository
{
}