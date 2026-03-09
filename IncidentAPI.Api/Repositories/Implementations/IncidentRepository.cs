using Microsoft.EntityFrameworkCore;
using IncidentAPI.Api.Data;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;

namespace IncidentAPI.Api.Repositories.Implementations;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Incident>> GetAllAsync()
    {
        return await _context.Incidents
            .Include(i => i.User)
            .Include(i => i.Category)
            .Include(i => i.Comments)
            .ToListAsync();
    }

    public async Task<Incident?> GetByIdAsync(int id)
    {
        return await _context.Incidents
            .Include(i => i.User)
            .Include(i => i.Category)
            .Include(i => i.Comments)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Incident> CreateAsync(Incident incident)
    {
        _context.Incidents.Add(incident);
        await _context.SaveChangesAsync();
        return incident;
    }

    public async Task<Incident?> UpdateAsync (int id, Incident incident)
    {
        var existing = await _context.Incidents.FindAsync(id);
        if (existing == null) return null;

        existing.Title = incident.Title ?? existing.Title;
        existing.Status = incident.Status ?? existing.Status;
        existing.CategoryId = incident.CategoryId != 0 ? incident.CategoryId : existing.CategoryId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id){
        var existing = await _context.Incidents.FindAsync(id);
        if (existing == null) return false;

        _context.Incidents.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
        
    
}