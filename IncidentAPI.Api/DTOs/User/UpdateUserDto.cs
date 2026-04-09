using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.User;

/*
    Se usa en updates (PUT/PATCH)
    Permite modificar solo los campos necesarios
*/

// DTO para actualizar un usuario existente
public class UpdateUserDto
{
    public string ? Name { get; set; }

    [EmailAddress]
    public string ? Email { get; set; }
}