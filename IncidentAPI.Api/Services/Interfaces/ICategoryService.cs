using IncidentAPI.Api.DTOs.Category;

namespace IncidentAPI.Api.Services.Interfaces;

/*
    Service -> Contiene la logica de negocio
        - Usa el repositorio por debajo
        - Convierte entidades (category) -> DTOs
*/

// Interfaz que define la logica de negocio para categorias
public interface ICategoryService
{
    // Obtener todas las categorias (devuevel DTOs)
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    // Obtener una categoria por id
    Task<CategoryDto?> GetByIdAsync(int id);

    // Crear una categoria a partir de un DTO
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

    // Actualizar una categoria con datos del DTO
    Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto);

    // Eliminar una categoria
    Task<bool> DeleteAsync(int id);
}