using E_Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Library.Domain.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task GetAllUsers();
    Task DeleteUser(int id);
    Task GetUserById(int id);
}