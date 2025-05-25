using LAB08_FreddyQuea.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_FreddyQuea.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet("{price:decimal}")]
    public async Task<IActionResult> GetProductsByPrice([FromRoute] decimal price)
    {
        var products = await productService.GetProductsByPriceAsync(price);
        return Ok(products);
    }
    
    [HttpGet("exercise5")]
    public async Task<IActionResult> GetMostExpensiveProduct()
    {
        var product = await productService.GetMostExpensiveProductAsync();
        return Ok(product);
    }
    
    [HttpGet("exercise7")]
    public async Task<IActionResult> GetAverageProductPrice()
    {
        var average = await productService.GetAverageProductPriceAsync();
        return Ok(new {promedio = average});
    }
    
    [HttpGet("exercise8")]
    public async Task<IActionResult> GetProductsWithoutDescription()
    {
        var products = await productService.GetProductsWithoutDescriptionAsync();
        return Ok(products);
    }
    
    [HttpGet("exercise12/{productId}")]
    public async Task<IActionResult> GetCustomersByProductId(int productId)
    {
        var customers = await productService.GetCustomersByProductIdAsync(productId);
        return Ok(customers);
    }
}