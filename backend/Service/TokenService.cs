using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Model;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service;

public class TokenService
{

    private readonly string _issuer;
    
    private readonly string _secretKey;

    public TokenService(IConfiguration configuration)
    {
        _issuer = "pcForum";
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole),
            }),
            Expires = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(7)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _issuer,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true, 
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // This is the maximum allowable clock skew between the token's timestamps and the current time
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");

            return new ClaimsPrincipal(claimsIdentity);
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine(ex);
            throw new Exception("Token faulty");
        }
    }
    
    
}