namespace E_Library.Application.Services.Authentication;

public class VerifyPasswordHashServices
{
    public bool VerifyPasswordMatch(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}