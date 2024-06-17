using E_Library.Application.Services;
using E_Library.Domain.Entities;
using E_Library.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers;

[ApiController]
[Route("api/v1")]
public class UserControllers(UserServices userServices) : ControllerBase
{

    private readonly UserServices _userServices = userServices;

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userData)
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
        catch (Exception e)
        {
            return BadRequest(new { error = "Falha ao criar novo usuário", reason = e.Message });
        }
    }
}