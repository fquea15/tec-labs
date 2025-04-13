using AutoMapper;
using Server.DTOs.OrderDetail;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Utils;

namespace Server.Services.Implementations;

public class OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderDetailService
{
    public async Task<IEnumerable<GetOrderDetail>> GetAllAsync()
    {
        var details = await unitOfWork.OrderDetails.GetAllAsync();
        return mapper.Map<IEnumerable<GetOrderDetail>>(details);
    }

    public async Task<GetOrderDetail?> GetByIdAsync(int id)
    {
        var detail = await unitOfWork.OrderDetails.GetByIdAsync(id);
        return detail == null ? null : mapper.Map<GetOrderDetail>(detail);
    }

    public async Task<ServiceResponse> AddAsync(CreateOrderDetail orderDetail)
    {
        var entity = mapper.Map<OrderDetail>(orderDetail);
        await unitOfWork.OrderDetails.AddAsync(entity);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Detalle de orden registrado correctamente.")
            : new ServiceResponse(false, "No se pudo registrar el detalle.");
    }

    public async Task<ServiceResponse> UpdateAsync(int id, UpdateOrderDetail orderDetail)
    {
        var existing = await unitOfWork.OrderDetails.GetByIdAsync(id);

        mapper.Map(orderDetail, existing);
        await unitOfWork.OrderDetails.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Detalle actualizado correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar el detalle.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.OrderDetails.GetByIdAsync(id);

        await unitOfWork.OrderDetails.DeleteAsync(existing.OrderDetailId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Detalle eliminado correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar el detalle.");
    }
}