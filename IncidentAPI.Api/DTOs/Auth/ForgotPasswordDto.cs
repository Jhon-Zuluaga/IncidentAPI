
/*
    DTO simple para solicitar recuperacion de contraseña
        - Solo tiene el email del usuario
        - Se usa en el endpoint: forgot-password
        - El backend usa este email para buscar el usuario
*/  

public record ForgotPasswordDto(string Email);