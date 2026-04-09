namespace IncidentAPI.Api.DTOs.Incident;

/* 
     DTO -> Se usa para exponer solo los datos necesarios
            (Incluye datos relacionados usuario y categoria)
            No incluye logica ni datos sensibles
*/

// DTO que representa un incidente (datos que se envian al cliente)
public class IncidentDto
{
     public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

}