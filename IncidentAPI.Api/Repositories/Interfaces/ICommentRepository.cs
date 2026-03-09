using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllByIncidentIdAsync(int incidentId);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, Comment comment);
    Task<bool> DeleteAsync(int id);
}