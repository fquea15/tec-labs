using Server.DTOs.OrderDetail;
using Server.DTOs.Payment;

namespace Server.DTOs.Order;

public class GetOrder : OrderBase
{
    public int OrderId { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<GetOrderDetail> OrderDetails { get; set; } = new();
    public List<GetPayment> Payments { get; set; } = new();
}