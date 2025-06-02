using LAB08_FreddyQuea.DTOs.Order;

namespace LAB08_FreddyQuea.DTOs.Customer;

public class GetCustomerOrders
{
    public string CustomerName { get; set; }
    public List<GetOrder> Orders { get; set; }
}