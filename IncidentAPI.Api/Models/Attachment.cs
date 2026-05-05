using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncidentAPI.Api.Models;


/*
    Model -> representa una tabla en la BD
    Relacion muchos a uno (muchos comentarios -> un incident)
*/

// Modelo que representa un comentario en la base de datos
public class Attachment
{
    [Key]
    public int Id { get; set; }
    
    // Nombre original del archivo
    [Required]
    public string FileName { get; set; } = string.Empty;

    // GUID + extensión (el que se guarda en disco)
    [Required]
    public string StoredFileName { get; set; } = string.Empty;

    // Ruta fisica en el servidor
    [Required]
    public string FilePath { get; set; } = string.Empty;

    // img/png, application/pdf, etc
    [Required]
    public string ContentType { get; set; } = string.Empty;

    // Tamaño en bytes
    [Required]
    public long FileSize { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    // Relacion con Incident
    [ForeignKey("Incident")]
    public int IncidentId { get; set; }
    public Incident Incident { get; set; } = null!;

}