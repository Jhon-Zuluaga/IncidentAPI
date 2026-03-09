using IncidentAPI.Api.DTOs.Category;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryService(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _CategoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _CategoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
        };

        var created = await _CategoryRepository.CreateAsync(category);

        return new CategoryDto
        {
            Id = created.Id,
            Name = created.Name,
        };
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name ?? string.Empty,
        };

        var updated = await _CategoryRepository.UpdateAsync(id, category);
        if (updated == null) return null;

        return new CategoryDto
        {
            Id = updated.Id,
            Name = updated.Name,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _CategoryRepository.DeleteAsync(id);
    }
}