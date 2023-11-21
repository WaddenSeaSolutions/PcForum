using System.Security.Authentication;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace backend.ActionFilter;

public class AuthenticationFilter : IActionFilter
{
    private readonly TokenService _tokenService;

    
    public AuthenticationFilter(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // var authHeaders = context.HttpContext.Request.Headers.Authorization;
        // if (authHeaders.IsNullOrEmpty() || authHeaders[0].IsNullOrEmpty())
        // {
        //     throw new AuthenticationException("No user authentication present");
        // }
        //
        // User user = _tokenService.ValidateAndReturnTokenIfUserIsNotDeleted(authHeaders);
        // context.HttpContext.Items["user"] = user;
    }


    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}