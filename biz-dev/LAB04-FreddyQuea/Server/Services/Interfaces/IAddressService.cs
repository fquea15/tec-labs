using Server.DTOs.Address;
using Server.Utils;

namespace Server.Services.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<GetAddress>> GetAllAsync();
    Task<GetAddress?> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateAddress address);
    Task<ServiceResponse> UpdateAsync(int id, UpdateAddress address);
    Task<ServiceResponse> DeleteAsync(int id);
}