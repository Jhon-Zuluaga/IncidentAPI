using IncidentAPI.Api.DTOs.Incident;

namespace IncidentAPI.Api.Services.Interfaces;

/*
    Service -> Contiene la logica de negocio
        - Usa el repositorio por debajo
        - Convierte entidades (Incident) -> DTOs
*/

// Interfaz que define la lógica de negocio para incidentes
public interface IIncidentService
{
    // Obtener todos los incidentes (devuelve DTOs)
    Task<IEnumerable<IncidentDto>> GetAllAsync();

    // Obtener un incidente por id
    Task<IncidentDto?> GetByIdAsync(int id);

    // Crear un incidente a partir de un DTO
    Task<IncidentDto> CreateAsync(CreateIncidentDto dto);

    // Actualizar un incidente con datos del DTO
    Task<IncidentDto?> UpdateAsync(int id, UpdateIncidentDto dto);

    // Eliminar un incidente
    Task<bool> DeleteAsync(int id);
}