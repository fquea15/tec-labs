namespace Server.DTOs.Address;

public class AddressBase
{
    public int CustomerId { get; set; }
    public string AddressLine { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? Type { get; set; }
}