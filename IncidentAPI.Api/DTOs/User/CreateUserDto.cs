using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.User;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}