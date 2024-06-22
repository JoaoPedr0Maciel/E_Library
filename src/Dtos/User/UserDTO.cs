namespace E_Library.Dtos.User;

public record UserDto(string Name, string Email, string Password, string Address)
{
    
}

public record LoginUserDto(string Email, string Password)
{

}
