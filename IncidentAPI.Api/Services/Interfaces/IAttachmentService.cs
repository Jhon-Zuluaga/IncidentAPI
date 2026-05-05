

public interface IAttachmentService
{
    Task<IEnumerable<AttachmentDto>> GetAllByIncidentIdAsync(int incidentId);
    Task<AttachmentDto?> GetByIdAsync(int id);
    Task<AttachmentDto?> UploadAsync(int incidentId, CreateAttachmentDto dto, string baseUrl); 
    Task<bool> DeleteAsync(int id);
}