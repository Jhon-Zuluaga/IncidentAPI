using System.ComponentModel.DataAnnotations;
namespace IncidentAPI.Api.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}