using AutoMapper;
using Server.DTOs.Order;
using Server.DTOs.OrderDetail;
using Server.DTOs.Payment;
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

        if (order == null)
        {
            return null;
        }

        var orderDetails = await unitOfWork.OrderDetails.GetByOrderIdAsync(id);

        var payments = await unitOfWork.Payments.GetByOrderIdAsync(id);

        var result = mapper.Map<GetOrder>(order);

        result.OrderDetails = mapper.Map<List<GetOrderDetail>>(orderDetails); 
        result.Payments = mapper.Map<List<GetPayment>>(payments);

        return result;
    }


    public async Task<ServiceResponse> AddAsync(CreateOrder order)
    {
        var entity = mapper.Map<Order>(order);

        var addresses = await unitOfWork.Addresses.GetAddressesByCustomerIdAsync(entity.CustomerId);

        var enumerable = addresses as Address[] ?? addresses.ToArray();
        if (!enumerable.Any())
        {
            return new ServiceResponse(false, "El cliente no tiene direcciones registradas.");
        }

        var billingAddress = enumerable.FirstOrDefault(a => a.Type == "billing");
        if (billingAddress != null)
        {
            entity.BillingAddressId = billingAddress.AddressId;
        }
        else
        {
            return new ServiceResponse(false, $"La dirección de facturación no está registrada.");
        }

        var shippingAddress = enumerable.FirstOrDefault(a => a.Type == "shipping");
        if (shippingAddress != null)
        {
            entity.ShippingAddressId = shippingAddress.AddressId;
        }
        else
        {
            return new ServiceResponse(false, "La dirección de envío no está registrada.");
        }

        unitOfWork.Orders.AddAsync(entity);

        var saved = await unitOfWork.CompleteAsync();

        if (saved > 0)
        {
            foreach (var orderDetail in order.OrderDetails.Select(mapper.Map<OrderDetail>))
            {
                orderDetail.OrderId = entity.OrderId;
                unitOfWork.OrderDetails.AddAsync(orderDetail);
            }

            saved = await unitOfWork.CompleteAsync();
        }

        return saved > 0
            ? new ServiceResponse(true, "Orden registrada correctamente.")
            : new ServiceResponse(false, "No se pudo registrar la orden.");
    }


    public async Task<ServiceResponse> UpdateAsync(int id, UpdateOrder order)
    {
        var existing = await unitOfWork.Orders.GetByIdAsync(id);

        mapper.Map(order, existing);
        if (existing != null) unitOfWork.Orders.UpdateAsync(existing);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Orden actualizada correctamente.")
            : new ServiceResponse(false, "No se pudo actualizar la orden.");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var existing = await unitOfWork.Orders.GetByIdAsync(id);

        if (existing != null) unitOfWork.Orders.DeleteAsync(existing.OrderId);
        var saved = await unitOfWork.CompleteAsync();

        return saved > 0
            ? new ServiceResponse(true, "Orden eliminada correctamente.")
            : new ServiceResponse(false, "No se pudo eliminar la orden.");
    }
}