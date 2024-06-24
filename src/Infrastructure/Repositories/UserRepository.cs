using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using E_Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_Library.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<User> CreateUserAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        // return users without password
        return await _appDbContext.Users.Select(user => new User
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdateAt = user.UpdateAt
        }).ToListAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    { 
       return await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
       
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _appDbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
         _appDbContext.Users.Update(user);
         await _appDbContext.SaveChangesAsync();

         return user;
    }
    
}
