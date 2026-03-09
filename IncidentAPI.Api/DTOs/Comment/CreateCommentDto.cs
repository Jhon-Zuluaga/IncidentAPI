using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.Comment;

public class CreateCommentDto
{
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public int IncidentId { get; set; }
}