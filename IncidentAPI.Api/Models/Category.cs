using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}