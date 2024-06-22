using E_Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Library.Domain.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
    Task DeleteUserAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> UpdateUserAsync(User user);
}