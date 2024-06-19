using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;

namespace E_Library.Application.Services;

public class UserServices(IUserRepository userRepository, AuthService authServices)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly AuthService _authService = authServices;
    public async Task<User> CreateUser(User user)
    {
        var hashedPassword = HashPassword(user.Password ?? string.Empty);

        var newUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = hashedPassword,
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

    public async Task<object> LoginUser(string email, string password)
    {
        var user = await _userRepository.LoginUserAsync(email);
        var token = _authService.GenerateTokenJwt(user);
        var isAuth = VerifyPasswordMatch(password, user.Password ?? string.Empty);

        if (!isAuth)
        {
            throw new Exception("Credenciais inválidas");
        }

        var userWhitOutPassword = new User()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Address = user.Address,
            CreatedAt = user.CreatedAt,
            UpdateAt = user.UpdateAt
        };

        return new
        {
            user = userWhitOutPassword,
            token
        };
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPasswordMatch(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}