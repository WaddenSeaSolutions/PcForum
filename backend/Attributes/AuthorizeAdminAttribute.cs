using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace backend.Attributes;

public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenService = (TokenService)context.HttpContext.RequestServices.GetService(typeof(TokenService));
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        try
        {
            // Validate the token and get the user
            var user = tokenService.validateTokenAndReturnUserIfNotDeleted(token);

            // Additional step to confirm the user has an admin role
            // Adjust this part based on how you determine whether a user is an admin
            if (user.UserRole == "admin")
            {
                context.HttpContext.Items["User"] = user;
                return;
            }

            // Store the user in HttpContext if you need it in your controller later
            context.Result = new UnauthorizedResult();
        }
        catch (SecurityTokenException)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}