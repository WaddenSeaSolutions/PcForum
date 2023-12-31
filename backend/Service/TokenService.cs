﻿using System.IdentityModel.Tokens.Jwt;
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
    private TokenDAL _tokenDal;
    
    private static readonly byte[] Secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtKey")!);

    public TokenService(TokenDAL tokenDal)
    {
        _tokenDal = tokenDal;
    }

    public string createToken(User user)
    {
        try
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

        
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            Console.WriteLine("An exeption happend when creating token: "+ e.Message);
            throw new Exception("Failed to create a token");
        }

    }

    public User validateTokenAndReturnUser(string token)
    {
        try
        {
            var principal = validateAndReturnToken(token); //Validating the token has not been tampered
    
            var nameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // Saves username of the user

            User userFromToken = _tokenDal.userFromUsername(nameClaim.Value);

            return userFromToken;
        }
        catch (Exception e)
        {
            Console.WriteLine("validateTokenAndReturnUser failed: " + e.Message);
            throw new Exception("Could not validate token");
        }
        
    }
    

    private ClaimsPrincipal validateAndReturnToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secret),
                ValidateIssuer = false, // No Issuer, not needed for this scale of program
                ValidateAudience = false, // No targeted audience
                ClockSkew = TimeSpan.Zero // Amount of time the token can be over date
            };

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }
        catch (Exception e)
        {
            Console.WriteLine("Token validation failed: " + e.Message);
            throw new Exception("Token validation failed");
        }
    }
    
}