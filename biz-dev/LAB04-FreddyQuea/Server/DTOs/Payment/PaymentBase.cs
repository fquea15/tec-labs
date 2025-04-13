namespace Server.DTOs.Payment;

public class PaymentBase
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
}