namespace Server.DTOs.Order;

public class OrderBase
{
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
}