using IncidentAPI.Api.DTOs.Comment;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;
using IncidentAPI.Api.Services.Interfaces;

namespace IncidentAPI.Api.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDto>> GetAllByIncidentIdAsync(int incidentId)
    {
        var comments = await _commentRepository.GetAllByIncidentIdAsync(incidentId);
        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Content = c.Content,
            Author = c.Author,
            CreatedAt = c.CreatedAt,
            IncidentId = c.IncidentId
        });
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return null;

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Author = comment.Author,
            CreatedAt = comment.CreatedAt,
            IncidentId = comment.IncidentId
        };
    }

    public async Task<CommentDto> CreateAsync(CreateCommentDto dto)
    {
        var comment = new Comment
        {
            Content = dto.Content,
            Author = dto.Author,
            IncidentId = dto.IncidentId
        };

        var created = await _commentRepository.CreateAsync(comment);

        return new CommentDto
        {
            Id = created.Id,
            Content = created.Content,
            Author = created.Author,
            CreatedAt = created.CreatedAt,
            IncidentId = created.IncidentId
        };
    }

    public async Task<CommentDto?> UpdateAsync(int id, UpdateCommentDto dto)
    {
        var comment = new Comment
        {
            Content = dto.Content ?? string.Empty,
            Author = dto.Author ?? string.Empty
        };

        var updated = await _commentRepository.UpdateAsync(id, comment);
        if (updated == null) return null;

        return new CommentDto
        {
            Id = updated.Id,
            Content = updated.Content,
            Author = updated.Author,
            CreatedAt = updated.CreatedAt,
            IncidentId = updated.IncidentId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _commentRepository.DeleteAsync(id);
    }
}