using IncidentAPI.Api.Models;

// Interfaz que define el contrato para generar tokens JWT
public interface IJwtService
{   
    // Método que recibe un usuario y devuelve un token JWT en string
    string GenerateToken(User user);
}