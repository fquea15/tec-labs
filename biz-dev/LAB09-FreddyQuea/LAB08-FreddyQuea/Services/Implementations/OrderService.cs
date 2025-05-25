using AutoMapper;
using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.DTOs.OrderDetail;
using LAB08_FreddyQuea.Repositories.Interfaces;
using LAB08_FreddyQuea.Services.Interfaces;

namespace LAB08_FreddyQuea.Services.Implementations;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
{
    public async Task<IEnumerable<GetOrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        return await unitOfWork.OrderRepository.GetOrderDetailsByOrderIdAsync(orderId);
    }

    public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
    {
        return await unitOfWork.OrderRepository.GetTotalQuantityByOrderIdAsync(orderId);
    }

    public async Task<IEnumerable<GetOrder>> GetOrdersAfterDateAsync(DateTime date)
    {
        var orders = await unitOfWork.OrderRepository.GetOrdersAfterDateAsync(date);
        return mapper.Map<IEnumerable<GetOrder>>(orders);
    }

    public async Task<IEnumerable<GetAllOrderDetail>> GetAllOrderDetailsAsync()
    {
        return await unitOfWork.OrderRepository.GetAllOrderDetailsAsync();
    }

    public async Task<IEnumerable<GetOrderWithDetails>> GetOrderWithDetailsAsync()
    {
        return await unitOfWork.OrderRepository.GetOrderWithDetailsAsync();
    }
}