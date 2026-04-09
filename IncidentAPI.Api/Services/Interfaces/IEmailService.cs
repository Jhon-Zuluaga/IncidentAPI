
/*
    Service -> Encargado del envio de correos electronicos
        - Define las operaciones relacionadas con email
        - Se usa para notificaciones del sistema
*/

// Interfaz para el servicio de correos
public interface IEmailService
{
    // Enviar correo cuando se crea un incidente
    //  - toEmail: correo del destinario
    //  - toName: nombre del usuario
    //  - IncidentTitle: titulo del incidente
    Task SendIncidentCreatedAsync(string toEmail, string toName, string incidentTitle);

    // Enviar correo para recuperacion de contraseña
    //  - toEmail: correo del usuario
    //  - toName: nombre del usuario
    //  - ResetToken: codigo/token de recuperacion
    Task SendPasswordResetAsync(string toEmail, string toName, string ResetToken);
}