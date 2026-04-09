using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Category;

/*
    Se usa cuando el cliente crea una categoria
    Incluye validaciones para asaegurar datos correctos
*/

// DTO para crear una nueva categoria
public class CreateCategoryDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z ]+$",
        ErrorMessage = "El nombre no puede contener números ni caracteres especiales")]
    public string Name { get; set; } = string.Empty;
}