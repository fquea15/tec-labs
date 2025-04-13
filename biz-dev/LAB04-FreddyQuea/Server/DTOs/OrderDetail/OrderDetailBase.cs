namespace Server.DTOs.OrderDetail;

public class OrderDetailBase
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}