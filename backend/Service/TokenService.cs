using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using backend.DAL;
using backend.Model;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service;

public class TokenService
{
    private TokenDal _tokenDal;

    private static readonly byte[] Secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtKey")!);

    public TokenService(TokenDal tokenDal)
    {
        _tokenDal = tokenDal;
    }

    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole),
            }),
            Expires = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(7)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature)
        };

        try
        {
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to create a token", e.InnerException);
        }

    }

    public User validateTokenAndReturnUserIfNotDeleted(string token)
    {
        var principal = ValidateAndReturnToken(token); //Validating the token has not been tampered
    
        var nameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // Saves the id of the user with the token

        User userFromToken = _tokenDal.userFromUsername(nameClaim.Value);

        Console.WriteLine(userFromToken.Email);
        if (userFromToken.Deleted == false)
            return userFromToken;

        throw new Exception("User is deleted");
    }
    

    private ClaimsPrincipal ValidateAndReturnToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secret),
                ValidateIssuer = false, // No Issuer
                ValidateAudience = false, // No targeted audience
                ClockSkew = TimeSpan.Zero // Amount the token can be over date
            };

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }
        catch (Exception ex)
        {
            // Token validation failed
            throw new Exception("Token validation failed", ex);
        }
    }
    
}