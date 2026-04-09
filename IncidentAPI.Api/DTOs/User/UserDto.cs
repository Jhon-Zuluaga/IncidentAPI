namespace IncidentAPI.Api.DTOs.User;

/*
    DTO -> Se usa para exponer solo los datos necesarios
            No incluye logica ni datos sensibles
*/

// DTO que representa un usuario (datos que se envian al cliente)
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}