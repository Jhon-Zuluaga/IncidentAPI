using Microsoft.AspNetCore.Mvc;
using IncidentAPI.Api.DTOs.Comment;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _CommentService;

    public CommentController(ICommentService CommentService)
    {
        _CommentService = CommentService;
    }

    [HttpGet("incident/{incidentId}")]
    public async Task<IActionResult> GetAllByIncidentId(int incidentId)
    {
        var comments = await _CommentService.GetAllByIncidentIdAsync(incidentId);
        if (!comments.Any()) return NotFound("No hay información suministrada");
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _CommentService.GetByIdAsync(id);
        if (comment == null) return NotFound($"Comentario con id {id} no encontrado");
        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _CommentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _CommentService.UpdateAsync(id, dto);
        if(updated == null) return NotFound($"Comentario con id {id} no encontrado");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _CommentService.DeleteAsync(id);
        if (!deleted) return NotFound($"Comentario con id {id} no encontrado");
        return Ok($"Comentario con id {id} eliminado correctamente");
    }
}