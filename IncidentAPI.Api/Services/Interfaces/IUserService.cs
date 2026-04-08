using IncidentAPI.Api.DTOs.User;
using IncidentAPI.Api.Models;

namespace IncidentAPI.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);
    Task<User?> ValidateCredentialsAsync(string email, string password);
    Task<bool> DeleteAsync(int id);
}