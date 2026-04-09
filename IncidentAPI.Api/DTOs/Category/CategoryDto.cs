namespace IncidentAPI.Api.DTOs.Category;

/*
    DTO -> Se usa para exponer solo los datos necesarios
           No incluye logica ni propiedades sensibles
*/

// DTO que representa una categoria (datos que se envian al cliente)
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
