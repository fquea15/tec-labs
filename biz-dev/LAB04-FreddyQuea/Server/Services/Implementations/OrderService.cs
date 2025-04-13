using AutoMapper;
using Server.DTOs.Order;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
{
    public async Task<IEnumerable<GetOrder>> GetAllAsync()
    {
        var orders = await unitOfWork.Orders.GetAllAsync();
        return mapper.Map<IEnumerable<GetOrder>>(orders);
    }

    public async Task<GetOrder?> GetByIdAsync(int id)
    {
        var order = await unitOfWork.Orders.GetByIdAsync(id);
        return mapper.Map<GetOrder>(order);
    }

    public async Task<ServiceResponse> AddAsync(CreateOrder order)
    {
        var entity = mapper.Map<Order>(order);
        await unitOfWork.Orders.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Orden registrada correctamente.")
            : new ServiceResponse(false, "No se pudo registrar la orden.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateOrder order)
    {
        var existing = await unitOfWork.Orders.GetByIdAsync(id);

        mapper.Map(order, existing);
        await unitOfWork.Orders.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Orden actualizada correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar la orden.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Orders.GetByIdAsync(id);

        await unitOfWork.Orders.DeleteAsync(existing.OrderId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Orden eliminada correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar la orden.");
    }
}