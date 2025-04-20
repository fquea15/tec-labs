using AutoMapper;
using Server.DTOs.Customer;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
{
    public async Task<IEnumerable<GetCustomer>> GetAllAsync()
    {
        var customers = await unitOfWork.Customers.GetAllAsync();
        return mapper.Map<IEnumerable<GetCustomer>>(customers);
    }

    public async Task<GetCustomer?> GetByIdAsync(int id)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(id);
        return mapper.Map<GetCustomer>(customer);
    }

    public async Task<ServiceResponse> AddAsync(CreateCustomer customer)
    {
        var entity = mapper.Map<Customer>(customer);
        unitOfWork.Customers.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Cliente registrado correctamente.")
            : new ServiceResponse(false, "No se pudo registrar el cliente.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateCustomer customer)
    {
        var existing = await unitOfWork.Customers.GetByIdAsync(id);

        mapper.Map(customer, existing);
        if (existing != null) unitOfWork.Customers.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Cliente actualizado correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar el cliente.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Customers.GetByIdAsync(id);

        if (existing != null) unitOfWork.Customers.DeleteAsync(existing.CustomerId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Cliente eliminado correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar el cliente.");
    }
}