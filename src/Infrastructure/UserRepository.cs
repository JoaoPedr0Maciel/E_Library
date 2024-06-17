using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using E_Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Library.Infrastructure;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        var userExist = await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (userExist != null)
        {
            throw new Exception("Usuário já cadastrado");
        }
        
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        
        return user;
    }

    public async Task GetAllUsers()
    {
        await appDbContext.Users.ToListAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await appDbContext.Users.FindAsync(id);
        if (user != null)
        {
          appDbContext.Users.Remove(user);
          await appDbContext.SaveChangesAsync();
        }
        
    }

    public async Task GetUserById(int id)
    {
        await appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }
}