namespace LAB08_FreddyQuea.DTOs.Product;

public class GetProductsAbovePrice
{
    public int Productid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }
}