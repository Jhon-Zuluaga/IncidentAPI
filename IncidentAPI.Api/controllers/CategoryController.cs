using Microsoft.AspNetCore.Mvc;
using IncidentAPI.Api.DTOs.Category;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _CategoryService;

    public CategoryController(ICategoryService CategoryService)
    {
        _CategoryService = CategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _CategoryService.GetAllAsync();
        if (!categories.Any()) return NotFound("No hay información suministrada");
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _CategoryService.GetByIdAsync(id);
        if (category == null) return NotFound($"Categoria con id {id} no encontrada");
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _CategoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _CategoryService.UpdateAsync(id, dto);
        if(updated == null) return NotFound($"Categoria con id {id} no encontrada");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _CategoryService.DeleteAsync(id);
        if (!deleted) return NotFound($"Categoria con id {id} no encontrada");
        return Ok($"Categoria con id {id} eliminado correctamente");
    }
}