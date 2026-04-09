using IncidentAPI.Api.Models;


namespace IncidentAPI.Api.Repositories.Interfaces;


/*
    Repository -> Se encarga de la comunicación con la base de datos
                  Solo define que operaciones existen, no como se implementan   
*/


// Interfaz que define las operaciones de acceso a datos para usuarios
public interface IUserRepository
{
    // Obtener todos los usuarios
    Task<IEnumerable<User>> GetAllAsync();

    // Obtener un usuario por id
    Task<User?> GetByIdAsync(int id);

    // Crear un nuevo usuario
    Task<User> CreateAsync(User user);

    // Actualizar un usuario existente
    Task<User?> UpdateAsync(int id, User user);

    // Buscar un usuario por email (usado en el login JWT)
    Task<User?> GetByEmailAsync(string email);

    // Eliminar un usuario por id
    Task<bool> DeleteAsync(int id);
}