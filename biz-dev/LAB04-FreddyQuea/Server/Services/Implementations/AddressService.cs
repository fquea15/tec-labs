using AutoMapper;
using Server.DTOs.Address;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class AddressService(IUnitOfWork unitOfWork, IMapper mapper) : IAddressService
{
    public async Task<IEnumerable<GetAddress>> GetAllAsync()
    {
        var addresses = await unitOfWork.Addresses.GetAllAsync();
        return mapper.Map<IEnumerable<GetAddress>>(addresses);
    }

    public async Task<GetAddress?> GetByIdAsync(int id)
    {
        var address = await unitOfWork.Addresses.GetByIdAsync(id);
        return mapper.Map<GetAddress>(address);
    }

    public async Task<ServiceResponse> AddAsync(CreateAddress address)
    {
        var entity = mapper.Map<Address>(address);
        unitOfWork.Addresses.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Dirección registrada correctamente.")
            : new ServiceResponse(false, "No se pudo registrar la dirección.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateAddress address)
    {
        var existing = await unitOfWork.Addresses.GetByIdAsync(id);

        mapper.Map(address, existing);
        if (existing != null) unitOfWork.Addresses.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Dirección actualizada correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar la dirección.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Addresses.GetByIdAsync(id);

        if (existing != null) unitOfWork.Addresses.DeleteAsync(existing.AddressId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Dirección eliminada correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar la dirección.");
    }
}