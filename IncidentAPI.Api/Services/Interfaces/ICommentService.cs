using IncidentAPI.Api.DTOs.Comment;

namespace IncidentAPI.Api.Services.Interfaces;

/*
    Service -> Contiene la lógica de negocio
        - Usa el repositorio por debajo
        - Convierte entidades (comment) -> DTOs
*/

// Interfaz que define la logica de negocio para comentarios
public interface ICommentService
{
    // Obtener todos los comentarios de un incidente 
    Task<IEnumerable<CommentDto>> GetAllByIncidentIdAsync(int incidentId);

    // Obtener un comentario por id
    Task<CommentDto?> GetByIdAsync(int id);

    // Crear un comentario a partir de un DTO
    Task<CommentDto> CreateAsync(CreateCommentDto dto);

    // Actualizar un comentario con datos del DTO
    Task<CommentDto?> UpdateAsync(int id, UpdateCommentDto dto);

    // Eliminar un comentario
    Task<bool> DeleteAsync(int id);
}