using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
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

            // Store the user in HttpContext, so we can retrieve it later on
            context.HttpContext.Items["User"] = user;
        }
        catch (SecurityTokenException)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}