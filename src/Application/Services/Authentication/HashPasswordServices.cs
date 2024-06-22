namespace E_Library.Application.Services.Authentication;

public class HashPasswordServices
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}