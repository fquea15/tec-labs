using Server.DTOs.Product;

namespace Server.DTOs.Customer;

public class GetCustomer : CustomerBase
{
    public int CustomerId { get; set; }
}