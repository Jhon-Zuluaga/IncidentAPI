using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z ]+$",
        ErrorMessage = "El nombre no puede contener números ni caracteres especiales")]
    public string Name { get; set; } = string.Empty;
}