using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentAPI.Api.Models;

public class Incident
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "abierto";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}