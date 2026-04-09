using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Repositories.Interfaces;

/*
    Repository -> Se encarga de la comunicación con la base de datos
                  Solo define que operaciones existen, no como se implementan   
*/

// Interfaz que define las operaciones de acceso a datos para categorias
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);
}