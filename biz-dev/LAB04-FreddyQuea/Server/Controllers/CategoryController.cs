using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Category;
using Server.Services.Interfaces;

namespace Server.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var category = await service.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategory category)
    {
        var result = await service.AddAsync(category);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategory category)
    {
        var result = await service.UpdateAsync(id, category);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await service.DeleteAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}