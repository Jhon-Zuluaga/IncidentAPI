
public interface IAttachmentRepository
{
    Task<IEnumerable<Attachment>> GetAllByIncidentIdAsync(int incidentId);
    Task<Attachment?> GetByIdAsync(int id);
    Task<Attachment> CreateAsync(Attachment attachment);
    Task<bool> DeleteAsync(int id);
}