using E_Library.Application.Services;
using E_Library.Domain.Entities;
using E_Library.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserControllers(UserServices userServices) : ControllerBase
{

    private readonly UserServices _userServices = userServices;

    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Body inválido" });
        }

        var user = new User()
        {
            Name = userData.Name,
            Email = userData.Email,
            Password = userData.Password,
            Address = userData.Address,
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
}