namespace E_Library.Application.Services.Authentication;

public class PasswordServices
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public bool VerifyPasswordMatch(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}