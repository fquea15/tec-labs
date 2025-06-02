using LAB08_FreddyQuea.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_FreddyQuea.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<IActionResult> GetCustomersByName([FromRoute] string name)
    {
        var customers = await customerService.GetCustomersByNameAsync(name);
        return Ok(customers);
    }

    [HttpGet("exercise9")]
    public async Task<IActionResult> GetCustomerWithMostOrders()
    {
        var customer = await customerService.GetCustomerWithMostOrdersAsync();
        return Ok(customer);
    }

    [HttpGet("exercise11/{customerId:int}")]
    public async Task<IActionResult> GetProductsSoldToCustomer([FromRoute] int customerId)
    {
        var products = await customerService.GetProductsSoldToCustomerAsync(customerId);
        return Ok(products);
    }
    
    [HttpGet("with-orders")]
    public async Task<IActionResult> GetCustomersWithOrders()
    {
        var customers = await customerService.GetCustomersWithOrdersAsync();
        return Ok(customers);
    }
    
    [HttpGet("with-product-count")]
    public async Task<IActionResult> GetCustomersWithProductCount()
    {
        var customers = await customerService.GetCustomersWithProductCountAsync();
        return Ok(customers);
    }
    
    [HttpGet("sales")]
    public async Task<IActionResult> GetCustomerSales()
    {
        var sales = await customerService.GetCustomerSalesAsync();
        return Ok(sales);
    }
}