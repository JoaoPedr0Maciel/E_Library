using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Application.Services;

public class UserServices(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> CreateUser(User user)
    {
        var newUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Address = user.Address,
        };

        try
        {
           await _userRepository.CreateUserAsync(newUser);
           return newUser;
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
        
    }
}