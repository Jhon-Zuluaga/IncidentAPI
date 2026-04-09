using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentAPI.Api.Models;

/*
    Model -> representa una tabla en la BD
    Relacion muchos a uno (muchos comentarios -> un incident)
*/

// Modelo que representa un comentario en la base de datos
public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Clave foránea hacia Incident
    [Required]
    [ForeignKey("Incident")]
    public int IncidentId { get; set; }

    // Relación: comentario pertenece a un incidente
    public Incident Incident { get; set; } = null!;
}