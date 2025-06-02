namespace LAB08_FreddyQuea.DTOs.Order;

public class GetOrder
{
    public int Orderid { get; set; }

    public int? Clientid { get; set; }

    public DateTime Orderdate { get; set; }
}