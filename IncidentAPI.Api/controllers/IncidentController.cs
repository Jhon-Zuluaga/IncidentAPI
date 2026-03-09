using Microsoft.AspNetCore.Mvc;
using IncidentAPI.Api.DTOs.Incident;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly IIncidentService _IncidentService;

    public IncidentController(IIncidentService IncidentService)
    {
        _IncidentService = IncidentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var incidents = await _IncidentService.GetAllAsync();
        if (!incidents.Any()) return NotFound("No hay información suministrada");
        return Ok(incidents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var incident = await _IncidentService.GetByIdAsync(id);
        if (incident == null) return NotFound($"Incidente con id {id} no encontrado");
        return Ok(incident);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIncidentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _IncidentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateIncidentDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _IncidentService.UpdateAsync(id, dto);
        if(updated == null) return NotFound($"Incidente con id {id} no encontrado");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _IncidentService.DeleteAsync(id);
        if (!deleted) return NotFound($"Incidente con id {id} no encontrado");
        return Ok($"Incidente con id {id} eliminado correctamente");
    }
}