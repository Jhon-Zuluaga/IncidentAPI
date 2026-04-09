using Microsoft.EntityFrameworkCore;
using IncidentAPI.Api.Data;
using IncidentAPI.Api.Models;
using IncidentAPI.Api.Repositories.Interfaces;

namespace IncidentAPI.Api.Repositories.Implementations;

/*
    Este repositorio: 
        - Usa entity framework para acceder a la BD
        - Maneja CRUD de usuarios
        - Gestiona tokens de recuperación (reset password)
*/

// Implementación del repositorio de usuarios usando Entity Framework
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Obtener todos los usuarios de la BD
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        // Buscar por id
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        // Agregar nuevo usuario
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User user)
    {
        // Buscar usuario existente
        var existing = await _context.Users.FindAsync(id);
        if (existing == null) return null;

        // Actualizar datos básicos si vienen en el request
        existing.Name = user.Name ?? existing.Name;
        existing.Email = user.Email ?? existing.Email;

        // Actualizar contraseña solo si se envia
        if(!string.IsNullOrEmpty(user.PasswordHash))
            existing.PasswordHash = user.PasswordHash;
        
        existing.ResetToken = user.ResetToken;
        existing.ResetTokenExpiry = user.ResetTokenExpiry;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {   
        // Buscar usuario
        var existing = await _context.Users.FindAsync(id);
        if (existing == null) return false;

        // Eliminar usuario
        _context.Users.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        // Buscar usuario por email(login/recuperación)
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}