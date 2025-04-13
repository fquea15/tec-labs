using Server.DTOs.OrderDetail;

namespace Server.DTOs.Order;

public class CreateOrder : OrderBase
{
    public List<CreateOrderDetail> OrderDetails { get; set; } = new();
}