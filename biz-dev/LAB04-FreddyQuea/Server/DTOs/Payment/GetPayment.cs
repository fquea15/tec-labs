namespace Server.DTOs.Payment;

public class GetPayment : PaymentBase
{
    public int PaymentId { get; set; }
    public DateTime? PaidAt { get; set; }
}