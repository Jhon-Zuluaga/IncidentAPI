using Microsoft.AspNetCore.Mvc;
using IncidentAPI.Api.DTOs.User;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        if(!users.Any())return NotFound("No hay información suministrada");
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (User == null) return NotFound($"Usuario con id {id} no encontrado");
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _userService.UpdateAsync(id, dto);
        if(updated == null) return NotFound($"Usuario con id {id} no encontrado");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted) return NotFound($"Usuario con id {id} no encontrado");
        return Ok($"Usuario con id {id} eliminado correctamente");
    }
}