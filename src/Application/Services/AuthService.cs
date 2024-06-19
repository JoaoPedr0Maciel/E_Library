using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Library.Domain.Entities;
using E_Library.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace E_Library.Application.Services;

public class AuthService
{
    public string GenerateTokenJwt(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(ConfigurationJwtSecret.Key);
        
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );

        var claimsIdentity = GetClaims(user);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claimsIdentity,
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2)
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        
        var strToken = handler.WriteToken(token);

        return strToken;
    }

    private ClaimsIdentity GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return new ClaimsIdentity(claims);
    }
}