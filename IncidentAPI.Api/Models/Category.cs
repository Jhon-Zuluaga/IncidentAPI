using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.Models;

/*
    Model → representa una tabla en la BD
    Incidents → navegación (relación 1 a muchos)
*/

// Modelo que representa una categoria en la base de datos
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    // Relación: una categoría puede tener muchos incidentes
    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}