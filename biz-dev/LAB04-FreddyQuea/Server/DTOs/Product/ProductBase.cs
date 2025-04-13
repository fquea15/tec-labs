namespace Server.DTOs.Product;

public class ProductBase
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImgUrl { get; set; }
    public int? CategoryId { get; set; }
}