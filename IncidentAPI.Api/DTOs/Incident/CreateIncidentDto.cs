using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Incident;

public class CreateIncidentDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [RegularExpression("abierto|en_progreso|cerrado",
        ErrorMessage = "El estado debe ser: abierto, en_progreso o cerrado")]
     public string Status { get; set; } = "abierto";

     [Required]
     public int UserId { get; set; }
     [Required]
     public int CategoryId {get; set; }

}