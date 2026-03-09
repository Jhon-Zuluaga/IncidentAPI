using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentAPI.Api.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    [ForeignKey("Incident")]
    public int IncidentId { get; set; }
    public Incident Incident { get; set; } = null!;
}