using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using E_Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Library.Infrastructure.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task GetAllUsersAsync()
    {
        await appDbContext.Users.ToListAsync();
    }

    public async Task  DeleteUserAsync(int id)
    {
        await appDbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    { 
       return await appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
       
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await appDbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
    }
}