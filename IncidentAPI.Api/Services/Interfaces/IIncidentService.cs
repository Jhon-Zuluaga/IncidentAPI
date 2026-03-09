using IncidentAPI.Api.DTOs.Incident;

namespace IncidentAPI.Api.Services.Interfaces;

public interface IIncidentService
{
    Task<IEnumerable<IncidentDto>> GetAllAsync();
    Task<IncidentDto?> GetByIdAsync(int id);
    Task<IncidentDto> CreateAsync(CreateIncidentDto dto);
    Task<IncidentDto?> UpdateAsync(int id, UpdateIncidentDto dto);
    Task<bool> DeleteAsync(int id);
}