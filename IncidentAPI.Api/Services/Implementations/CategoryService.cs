using IncidentAPI.Api.DTOs.Category;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Services.Implementations;

/*
    Este service:
        - Maneja la logica de negocio
        - Convierte category -> DTO
        - Usa el repository para acceder a la BD
*/

// Implementación de la logica de negocio para categorias
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryService(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        // Obtener categorias
        var categories = await _CategoryRepository.GetAllAsync();

        // Convertir a DTO
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        // Buscar categoria por id
        var category = await _CategoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        // Mapear a DTO
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        // Crear entidad Category
        var category = new Category
        {
            Name = dto.Name,
        };

        // Guardar en BD
        var created = await _CategoryRepository.CreateAsync(category);

        // Retornar DTO
        return new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
        };
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        // Si el nombre está vacío o es nulo no actualiza
        if (string.IsNullOrWhiteSpace(dto.Name))
            return null;

        var category = new Category
        {
            Name = dto.Name
        };

        // Actualizar en BD
        var updated = await _CategoryRepository.UpdateAsync(id, category);
        if (updated == null) return null;

        // Retornar DTO
        return new CategoryDto
        {
            Id = updated.Id,
            Name = updated.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Eliminar categoria
        return await _CategoryRepository.DeleteAsync(id);
    }
}