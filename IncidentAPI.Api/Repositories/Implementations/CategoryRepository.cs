using Microsoft.EntityFrameworkCore;
using IncidentAPI.Api.Data;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;

namespace IncidentAPI.Api.Repositories.Implementations;

/*
    Este repositorio:
        - Usa entity framework para acceder a la BD
        - Maneja CRUD de categorias
*/

// Implementación del repositorio de categorias usando Entity Framework
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        // Obtener todas las categorias
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        // Buscar categorias por id
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        // Agregar nueva categoria
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        // Buscar categoria existente
        var existing = await _context.Categories.FindAsync(id);
        if (existing == null) return null;

        // Actualizar nombre si se envia
        existing.Name = category.Name ?? existing.Name;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Buscar categoria
        var existing = await _context.Categories.FindAsync(id);
        if (existing == null) return false;

        // Eliminar categoria
        _context.Categories.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }


}