using IncidentAPI.Api.DTOs.User;
using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Services.Interfaces;

/*
    Service -> Contiene la lógica de negocio
        - Usa el repositorio por debajo
        - Convierte entidades (User) -> DTOs

*/

// Interfaz que define la lógica de negocio para usuarios
public interface IUserService
{
    // Obtener todos los usuarios (devuelve DTOs)
    Task<IEnumerable<UserDto>> GetAllAsync();

    // Obtener un usuario por id
    Task<UserDto?> GetByIdAsync(int id);

    // Crear un usuario a partir de un DTO
    Task<UserDto> CreateAsync(CreateUserDto dto);

    // Actualizar un usuario con datos del DTO
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);

    // Validar credenciales (login)
    Task<User?> ValidateCredentialsAsync(string email, string password);

    // Eliminar un usuario
    Task<bool> DeleteAsync(int id);
}