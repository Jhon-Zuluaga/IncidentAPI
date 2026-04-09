using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Repositories.Interfaces;

/*
    Repository -> Se encarga de la comunicación con la base de datos
                  Solo define que operaciones existen, no como se implementan   
*/

// Interfaz que define las operaciones de acceso a datos para incidentes
public interface IIncidentRepository
{
    Task<IEnumerable<Incident>> GetAllAsync();
    Task<Incident?> GetByIdAsync(int id);
    Task<Incident> CreateAsync(Incident incident);
    Task<Incident?> UpdateAsync(int id, Incident incident);
    Task<bool> DeleteAsync(int id);
}