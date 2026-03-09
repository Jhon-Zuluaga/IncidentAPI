using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Incident;

public class UpdateIncidentDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    
    [Required]
    [RegularExpression("Abierto|en_progreso|cerrado",
        ErrorMessage = "El estado debe ser: abierto, en_progreso o cerrado")]
     public string? Status { get; set; } 
     

     public int? CategoryId {get; set; }
}