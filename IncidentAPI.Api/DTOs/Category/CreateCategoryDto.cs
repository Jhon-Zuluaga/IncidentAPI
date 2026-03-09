using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}