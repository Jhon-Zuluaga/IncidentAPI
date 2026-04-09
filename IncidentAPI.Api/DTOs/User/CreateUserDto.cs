using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.User;

/*
    Se usa cuando el cliente crea un usuario
    Incluye validaciones para asegurar datos correctos
*/

// DTO para crear un nuevo usuario
public class CreateUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}