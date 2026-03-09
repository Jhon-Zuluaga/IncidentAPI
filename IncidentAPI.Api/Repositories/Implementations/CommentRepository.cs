using Microsoft.EntityFrameworkCore;
using IncidentAPI.Api.Data;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;

namespace IncidentAPI.Api.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comment>> GetAllByIncidentIdAsync(int incidentId)
    {
        return await _context.Comments
        .Where(c => c.IncidentId == incidentId)
        .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync (int id, Comment comment)
    {
        var existing = await _context.Comments.FindAsync(id);
        if (existing == null) return null;

        existing.Content = comment.Content ?? existing.Content;
        existing.Author = comment.Author ?? existing.Author;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id){
        var existing = await _context.Comments.FindAsync(id);
        if (existing == null) return false;

        _context.Comments.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
        
    
}