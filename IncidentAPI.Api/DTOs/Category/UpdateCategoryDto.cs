namespace IncidentAPI.Api.DTOs.Category;

/*
    Se usa en Updates(PUT/PATCH)
    Permite enviar solo los campos que se quieren modificar
*/

// DTO para actualizar una categoria existente
public class UpdateCategoryDto
{
    public string? Name { get; set; }
}