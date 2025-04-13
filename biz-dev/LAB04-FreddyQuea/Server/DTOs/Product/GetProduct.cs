namespace Server.DTOs.Product;

public class GetProduct : ProductBase
{
    public int ProductId { get; set; }
    public string CategoryName { get; set; } = string.Empty;}