using E_Library.Application.Services.Authentication;
using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;

namespace E_Library.Application.Services.Users;

public class UserServices
{
    private readonly JwtServices _jwtServices;
    private readonly PasswordServices _passwordServices;
    private readonly IUserRepository _userRepository;

    public UserServices(JwtServices jwtServices,
        PasswordServices passwordServices,
        IUserRepository userRepository)
    {
        _jwtServices = jwtServices;
        _passwordServices = passwordServices;
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(User user)
    {
        var userExist = await _userRepository.GetUserByEmailAsync(user.Email!);
        if (userExist != null)
        {
            throw new Exception("Email ja cadastre");
        }
        var hashedPassword = _passwordServices.HashPassword(user.Password ?? string.Empty);

        var newUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = hashedPassword,
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
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("Email não cadastrado");
        }
        var token = _jwtServices.GenerateTokenJwt(user);
        var isAuth = _passwordServices.VerifyPasswordMatch(password, user.Password ?? string.Empty);

        if (!isAuth)
        {
            throw new Exception("Credenciais inválidas");
        }

        var userWhitOutPassword = new User()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdateAt = user.UpdateAt
        };

        return new
        {
            user = userWhitOutPassword,
            token
        };
        
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        if (users == null)
        {
            throw new Exception("Nenhum usuário cadastrado");
        }
        
        return users;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception("Usuário inválido, id não existe");
        }
        await _userRepository.DeleteUserAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user, int id)
    {
        var userExist = await _userRepository.GetUserByIdAsync(id);
        if (userExist == null)
        {
            throw new Exception("Usuário não encontrado para edição");
        }
    
        userExist.Name = user.Name;
        userExist.Email = user.Email;
        userExist.Password = _passwordServices.HashPassword(user.Password!);

        try
        {
            var userUpdated = await _userRepository.UpdateUserAsync(userExist);
            return userUpdated;
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao editar usuário", e);
        }
    }
}