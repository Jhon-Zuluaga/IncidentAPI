using IncidentAPI.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador encargado de la autenticación de usuarios (Login y logout)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _config;

    /// <summary>
    /// Constructor del controlador de autenticación.
    /// </summary>
    /// <param name="userService">Servicio para validar usuarios</param>
    /// <param name="jwtService">Servicio para generar tokens JWT</param>
    /// <param name="config">Configuración de la aplicación</param>
    public AuthController(IUserService userService, IJwtService jwtService, IConfiguration config)
    {
        _userService = userService;
        _jwtService = jwtService;
        _config = config;
    }

    /// <summary>
    /// Permite a un usuario iniciar sesión en el sistema
    /// </summary>
    /// <param name="dto">Objeto que contiene el email y contraseña del usuario</param>
    /// <returns>
    /// <response code="200">Si el login es exitoso y guarda token en una cookie</response>
    /// <response code="401">Unauthorized si las credenciales son inválidas</response>
    /// </returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {   
        //Validar credenciales del usuario
        var user = await _userService.ValidateCredentialsAsync(dto.email, dto.password);

        // Si no existe el usuario o las credenciales son incorrectas
        if (user == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        // Genera un token JWT
        var token = _jwtService.GenerateToken(user);

        // Obtener tiempo de expiración desde la configuración
        var expMinutes = double.Parse(_config["Jwt:ExpirationMinutes"]!);

        // Guardar el token en una cookie segura
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,       // Evitar acceso desde JavaScript
            Secure = false,        // En local false, en producción true(HTTPS)
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(expMinutes)
        });

        return Ok(new { message = "Login exitoso" });
    }

    /// <summary>
    /// Cierra la sesión del usuario autenticado eliminando la cookie JWT
    /// </summary>
    /// <returns>
    /// <response code="200">Cuando la sesión ha sido cerrado exitosa</response>
    /// </returns>
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {   
        //Eliminar la cookie que contiene el token JWT
        Response.Cookies.Delete("jwt");
        
        return Ok(new { message = "Sesión cerrada"});
    }
}