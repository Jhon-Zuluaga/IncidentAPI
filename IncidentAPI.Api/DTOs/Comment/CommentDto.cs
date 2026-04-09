namespace IncidentAPI.Api.DTOs.Comment;

/*
    DTO -> Se usa para exponer solo los datos necesarios
           No incluye logica ni datos sensibles
*/

// DTO que representa un comentario (datos que se envian al cliente)
public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int IncidentId { get; set; }
}