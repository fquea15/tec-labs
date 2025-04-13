using AutoMapper;
using Server.DTOs.Address;
using Server.DTOs.Category;
using Server.DTOs.Customer;
using Server.DTOs.Order;
using Server.DTOs.OrderDetail;
using Server.DTOs.Payment;
using Server.DTOs.Product;
using Server.Models;

namespace Server.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        // Product
        CreateMap<Product, GetProduct>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<CreateProduct, Product>();
        CreateMap<UpdateProduct, Product>();

        // Category
        CreateMap<Category, GetCategory>();
        CreateMap<CreateCategory, Category>();
        CreateMap<UpdateCategory, Category>();

        // Customer
        CreateMap<Customer, GetCustomer>();
        CreateMap<CreateCustomer, Customer>();
        CreateMap<UpdateCustomer, Customer>();

        // Address
        CreateMap<Address, GetAddress>();
        CreateMap<CreateAddress, Address>();
        CreateMap<UpdateAddress, Address>();

        // Payment
        CreateMap<Payment, GetPayment>();
        CreateMap<CreatePayment, Payment>();
        CreateMap<UpdatePayment, Payment>();

        // OrderDetail
        CreateMap<OrderDetail, GetOrderDetail>();
        CreateMap<CreateOrderDetail, OrderDetail>();
        CreateMap<UpdateOrderDetail, OrderDetail>();

        // Order
        CreateMap<Order, GetOrder>();
        CreateMap<CreateOrder, Order>();
        CreateMap<UpdateOrder, Order>();
    }
}