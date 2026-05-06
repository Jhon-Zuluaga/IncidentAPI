
using IncidentAPI.Api.Repositories.Interfaces;

public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IIncidentRepository _incidentRepository;
    private readonly IWebHostEnvironment _env;

    private static readonly string[] _allowedTyped =
        { "image/jpeg", "image/png", "/image/gif", "application/pdf", "text/plain"};
    private const long _maxFileSize = 5 * 1024 * 1024; // 5MB

    public AttachmentService(
        IAttachmentRepository attachmentRepository,
        IIncidentRepository incidentRepository,
        IWebHostEnvironment env)
    {
        _attachmentRepository = attachmentRepository;
        _incidentRepository = incidentRepository;
        _env = env;
    }

    public async Task<IEnumerable<AttachmentDto>> GetAllByIncidentIdAsync (int incidentId)
    {
        var attachments = await _attachmentRepository.GetAllByIncidentIdAsync(incidentId);
        return attachments. Select(a => ToDto(a));
    }

    public async Task<AttachmentDto?> GetByIdAsync(int id)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);
        return attachment is null ? null : ToDto(attachment);
    }

    public async Task<AttachmentDto?> UploadAsync(int incidentId, CreateAttachmentDto dto, string baseUrl)
    {
        // Verificar que el incidente existe
        var incident = await _incidentRepository.GetByIdAsync(incidentId);
        if(incident is null) return null;

        // Validar tipo de archivo
        if(!_allowedTyped.Contains(dto.File.ContentType)) return null;

        // Validar tamaño
        if(dto.File.Length > _maxFileSize) return null;

        // Generar nombre unico para no sobreescribir archivos con el mismo nombre
        var extension = Path.GetExtension(dto.File.FileName);
        var storedFileName = $"{Guid.NewGuid()}{extension}";

        // Crear la carpeta uploads si no existe
        var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var uploadsFolder = Path.Combine(webRoot, "uploads");
        Directory.CreateDirectory(uploadsFolder);

        // Guardar archivo en disco
        var filePath = Path.Combine(uploadsFolder, storedFileName);
        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        // Guardar el registro en la base de datos
        var attachment = new Attachment
        {
            FileName = dto.File.FileName,
            StoredFileName = storedFileName,
            FilePath = filePath,
            ContentType = dto.File.ContentType,
            FileSize = dto.File.Length,
            IncidentId = incidentId
        };

        var created = await _attachmentRepository.CreateAsync(attachment);
        return ToDto(created, baseUrl);
    }

    
    public async Task<bool> DeleteAsync(int id)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id);
        if(attachment is null) return false;

        // Eliminar el archivo fisico del disco.
        if(File.Exists(attachment.FilePath))
            File.Delete(attachment.FilePath);
        
        return await _attachmentRepository.DeleteAsync(id);
    }

    public static AttachmentDto ToDto(Attachment attachment, string baseUrl = "")
    {
        return new AttachmentDto
        {
            Id = attachment.Id,
            FileName = attachment.FileName,
            ContentType = attachment.ContentType,
            FileSize = attachment.FileSize,
            UploadedAt = attachment.UploadedAt,
            IncidentId = attachment.IncidentId,
            FileUrl = string.IsNullOrEmpty(baseUrl)
                ? attachment.StoredFileName
                : $"{baseUrl}/uploads/{attachment.StoredFileName}"
        };
    }
}