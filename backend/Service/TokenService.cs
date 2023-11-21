using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using backend.Model;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service;

public class TokenService
{

    private static readonly byte[] Secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtKey")!);

    public TokenService()
    {

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
    
    public User ValidateAndReturnTokenIfUserIsNotDeleted(StringValues authHeaders)
    {
        throw new NotImplementedException();
    }

    private JwtSecurityToken ValidateAndReturnToken(StringValues authHeader)
    {
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secret),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            new JwtSecurityTokenHandler().ValidateToken(authHeader[0],
                validationParameters,
                out SecurityToken token);
            
            var t =  (JwtSecurityToken)token;
            var json = JsonSerializer.Serialize(t.Claims);
            var role = t.Claims.FirstOrDefault(c => c.Type == "role");
            Console.WriteLine("the role is "+role);
            Console.WriteLine(json);
            return t;
        }
        catch (Exception e)
        {
            throw new AuthenticationException("Failed to validate user identity from token", e.InnerException);
        }

        /*
        public ClaimsPrincipal  ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
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
        */
        
    }
    
}