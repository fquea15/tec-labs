using LAB08_FreddyQuea.DTOs.OrderDetail;

namespace LAB08_FreddyQuea.DTOs.Order;

public class GetOrderWithDetails
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<GetOrderDetail> Products { get; set; }
}