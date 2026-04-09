
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IncidentAPI.Api.Models;
using Microsoft.IdentityModel.Tokens;

// Servicio que implementa la generación tokens JWT
public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

     public string GenerateToken(User user)
    {   
        // Obtener la clave secreta desde configuración
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        // Credenciales para firmar el token
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Información que se guarda dentro del token (claims)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Tiempo de expiración del token
        var expiration = DateTime.UtcNow.AddMinutes(
            double.Parse(_config["Jwt:ExpirationMinutes"]!));

        // Crear el token JWT con toda la información
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],      // Quien lo emite
            audience: _config["Jwt:Audience"],  // Para quien es
            claims: claims,                     // datos del usuario
            expires: expiration,                // expiración
            signingCredentials: credentials     // firma
        );

        // Convertir el token a string para enviarlo
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// Flujo:  user -> claims -> se firma -> se crea token -> se devuelve string