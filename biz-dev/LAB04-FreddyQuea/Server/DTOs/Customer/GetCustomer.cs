using Server.DTOs.Product;

namespace Server.DTOs.Customer;

public class GetCustomer : ProductBase
{
    public int CustomerId { get; set; }
}