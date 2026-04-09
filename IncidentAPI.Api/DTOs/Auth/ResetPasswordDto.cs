
/*
    DTO para restablecer la contraseña
        - Email: identifica al usuario
        - Token: codigo de verificacion enviado por correo
        - NewPassword: nueva contraseña a guardar

        Se usa en el endpoint: reset-password
*/

public record ResetPasswordDto(string Email, string Token, string NewPassword);