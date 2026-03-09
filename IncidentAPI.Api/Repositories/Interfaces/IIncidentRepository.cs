using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Repositories.Interfaces;

public interface IIncidentRepository
{
    Task<IEnumerable<Incident>> GetAllAsync();
    Task<Incident?> GetByIdAsync(int id);
    Task<Incident> CreateAsync(Incident incident);
    Task<Incident?> UpdateAsync(int id, Incident incident);
    Task<bool> DeleteAsync(int id);
}