using AutoMapper;
using LAB08_FreddyQuea.DTOs.Customer;
using LAB08_FreddyQuea.DTOs.Order;
using LAB08_FreddyQuea.DTOs.Product;
using LAB08_FreddyQuea.Models;

namespace LAB08_FreddyQuea.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Student
        CreateMap<Client, GetCustomerByNameAll>();
        CreateMap<Product, GetProductsAbovePrice>();
        CreateMap<Product, GetProduct>();
        CreateMap<Order, GetOrder>();
    }
}