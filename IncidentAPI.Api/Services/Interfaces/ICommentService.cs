using IncidentAPI.Api.DTOs.Comment;

namespace IncidentAPI.Api.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAllByIncidentIdAsync(int incidentId);
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto> CreateAsync(CreateCommentDto dto);
    Task<CommentDto?> UpdateAsync(int id, UpdateCommentDto dto);
    Task<bool> DeleteAsync(int id);
}