using E_Library.Application.Services;
using E_Library.Application.Services.Users;
using E_Library.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers.User;

[ApiController]
[Route("api/v1/users")]
public class UserControllers: ControllerBase
{

    private readonly UserServices _userServices;

    public UserControllers(UserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Body inválido" });
        }

        var user = new Domain.Entities.User()
        {
            Name = userData.Name,
            Email = userData.Email,
            Password = userData.Password,
        };

        try
        {
            var createdUser = await _userServices.CreateUser(user);
            return Created("v1/users", createdUser);
        }
        catch (Exception exception)
        {
            return BadRequest(new { error = "Falha ao criar novo usuário", reason = exception.Message });
        }
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto userData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new {error = "Os dados do body são inválidos"});
        }

        try
        {
            var data = await _userServices.LoginUser(userData.Email, userData.Password);
            return Ok(data);
        }
        catch (Exception exception)
        {
            return Unauthorized(new { error = "Falha ao efetuar login", reason = exception.Message });
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception exception)
        {
            return NotFound(new {error = exception.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] Domain.Entities.User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = "Body inválido" });

        try
        {
          var userUpdated = await _userServices.UpdateUserAsync(user, id);
          return Ok(userUpdated);
        }
        catch (Exception exception)
        {
            return BadRequest(new {error = exception.Message});
        }
    }    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
    {
        try
        {
            await _userServices.DeleteUserAsync(id);
            return Ok(new { deleted = $"Usuário com o id:{id} deletado com sucesso" });
        }
        catch (Exception exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }
    
    
    
}