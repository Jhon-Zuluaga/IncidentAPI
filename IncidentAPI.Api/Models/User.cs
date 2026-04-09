using System.ComponentModel.DataAnnotations;
namespace IncidentAPI.Api.Models;

/*
    Model -> representa una tabla en la BD
    PasswordHash -> nunca se guarda contraseña en texto plano
    ResetToken -> usado para recuperar contraseña
*/

// Modelo que representa un usuario en la base de datos
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    // Contraseña encriptada (hash)
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    // Token para recuperación de contraseña
    public string? ResetToken { get; set; }

    // Expiración del token de recuperación
    public DateTime? ResetTokenExpiry { get; set; }

    // Relación: un usuario puede tener muchos incidentes
    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}