using IncidentAPI.Api.DTOs.Incident;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Services.Implementations;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _IncidentRepository;

    public IncidentService(IIncidentRepository IncidentRepository)
    {
        _IncidentRepository = IncidentRepository;
    }

    public async Task<IEnumerable<IncidentDto>> GetAllAsync()
    {
        var incidents = await _IncidentRepository.GetAllAsync();
        return incidents.Select(i => new IncidentDto
        {
            Id = i.Id,
            Title = i.Title,
            Description = i.Description,
            Status = i.Status,
            CreatedAt = i.CreatedAt,
            UserId = i.UserId,
            UserName = i.User?.Name ?? string.Empty,
            CategoryId = i.CategoryId,
            CategoryName = i.Category?.Name ?? string.Empty
        });
    }

    public async Task<IncidentDto?> GetByIdAsync(int id)
    {
        var Incident = await _IncidentRepository.GetByIdAsync(id);
        if (Incident == null) return null;

        return new IncidentDto
        {
            Id = Incident.Id,
            Title = Incident.Title,
            Description = Incident.Description,
            Status = Incident.Status,
            CreatedAt = Incident.CreatedAt,
            UserId = Incident.UserId,
            UserName = Incident.User?.Name ?? string.Empty,
            CategoryId = Incident.CategoryId,
            CategoryName = Incident.Category?.Name ?? string.Empty
        };
    }

    public async Task<IncidentDto> CreateAsync(CreateIncidentDto dto)
    {
        var incident = new Incident
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId
        };

        var created = await _IncidentRepository.CreateAsync(incident);
        var full = await _IncidentRepository.GetByIdAsync(created.Id);

        return new IncidentDto
        {
            Id = full!.Id,
            Title = full.Title,
            Description = full.Description,
            Status = full.Status,
            CreatedAt = full.CreatedAt,
            UserId = full.UserId,
            UserName = full.User?.Name ?? string.Empty,
            CategoryId = full.CategoryId,
            CategoryName = full.Category?.Name ?? string.Empty
        };
    }

    public async Task<IncidentDto?> UpdateAsync(int id, UpdateIncidentDto dto)
    {
        var incident = new Incident
        {
            Title = dto.Title ?? string.Empty,
            Description = dto.Description ?? string.Empty,
            Status = dto.Status ?? string.Empty,
            CategoryId = dto.CategoryId.GetValueOrDefault()
        };

        var updated = await _IncidentRepository.UpdateAsync(id, incident);
        if (updated == null) return null;

        var full = await _IncidentRepository.GetByIdAsync(updated.Id);

        return new IncidentDto
        {
            Id = full!.Id,
            Title = full.Title,
            Description = full.Description,
            Status = full.Status,
            CreatedAt = full.CreatedAt,
            UserId = full.UserId,
            UserName = full.User?.Name ?? string.Empty,
            CategoryId = full.CategoryId,
            CategoryName = full.Category?.Name ?? string.Empty
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _IncidentRepository.DeleteAsync(id);
    }
}