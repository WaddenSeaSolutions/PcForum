using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Model;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service;

public class TokenService
{
    
    private readonly string _secretKey;

    public TokenService()
    {
        _secretKey = Environment.GetEnvironmentVariable("jwtKey"); // Environment variable for secret key to generate and decode tokens
    }
    
    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole), // User role
                // Add more claims as needed
            }),
            Expires = DateTime.UtcNow.AddDays(30), // Token expiration time set to 30 days
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}