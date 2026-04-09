using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Repositories.Interfaces;

/*
    Repository -> Se encarga de la comunicación con la base de datos
                  Solo define que operaciones existen, no como se implementan   
*/

//  Interfaz que define las operaciones de acceso a datos para comentarios
public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllByIncidentIdAsync(int incidentId);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, Comment comment);
    Task<bool> DeleteAsync(int id);
}