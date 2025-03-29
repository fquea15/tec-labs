using Lab2_FreddyQuea.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2_FreddyQuea.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
  private static List<Product> products = [];

  [HttpGet]
  public IActionResult GetAll() => Ok(products);

  [HttpPost]
  public IActionResult Add([FromBody] Product product)
  {
    products.Add(product);
    return Ok(new { message = "Producto agregado", products });
  }

  [HttpDelete("{index}")]
  public IActionResult Delete([FromRoute] int index)
  {
    if (index < 0 || index >= products.Count)
      return NotFound(new { message = "Indice fuera de rango" });

    products.RemoveAt(index);
    return Ok(new { message = "Producto eliminado", products });
  }
}