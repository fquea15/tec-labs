namespace Server.DTOs.Order;

public class OrderBase
{
    public int CustomerId { get; set; }
    public int ShippingAddressId { get; set; }
    public int BillingAddressId { get; set; }
    public decimal Total { get; set; }
}