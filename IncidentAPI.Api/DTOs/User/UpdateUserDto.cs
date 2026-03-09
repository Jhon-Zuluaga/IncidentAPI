using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Api.DTOs.User;

public class UpdateUserDto
{
    public string ? Name { get; set; }

    [EmailAddress]
    public string ? Email { get; set; }
}