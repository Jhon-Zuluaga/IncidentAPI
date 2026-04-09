using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentAPI.Api.Models;

/*
    Model -> representa una tabla en la BD
    Relaciones: USER (1:N), Category (1:N), Comments(1:N)
*/

// Modelo que representa un incidente en la base de datos
public class Incident
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "abierto";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Clave foránea hacia User
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    // Relación: incidente pertenece a un usuario
    public User User { get; set; } = null!;

    // Clave foránea hacia Category
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    // Relación: incidente pertenece a una categoría
    public Category Category { get; set; } = null!;
    
    // Relación: un incidente puede tener muchos comentarios
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}