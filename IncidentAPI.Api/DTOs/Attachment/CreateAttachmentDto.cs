

using System.ComponentModel.DataAnnotations;

/*
    IFormFile es la interfaz de .NET para manejar archivos multipart-form-data
    no JSON como los demas DTOs.
**/

public class CreateAttachmentDto
{
    [Required(ErrorMessage = "El archivo es obligatorio")]
    public IFormFile File { get; set; } = null!;
}