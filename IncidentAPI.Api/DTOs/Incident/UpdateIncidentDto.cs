using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Incident;

public class UpdateIncidentDto
{
    [MinLength(3, ErrorMessage = "El titulo debe tener al menos 3 caracteres")]
    [MaxLength(100, ErrorMessage = "El titulo no puede tener más de 100 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
        ErrorMessage = "El título solo puede contener letras")]
    public string? Title { get; set; }

    [MinLength(3, ErrorMessage = "La descripción debe tener al menos 3 caracteres")]
    [MaxLength(100, ErrorMessage = "La descripción no puede tener más de 100 caracteres")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$",
        ErrorMessage = "El título solo puede contener letras")]
    public string? Description { get; set; }
    
    [Required]
    [RegularExpression("Abierto|en_progreso|cerrado",
        ErrorMessage = "El estado debe ser: abierto, en_progreso o cerrado")]
     public string? Status { get; set; } 
     

     public int? CategoryId {get; set; }
}