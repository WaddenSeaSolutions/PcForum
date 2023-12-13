using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controller;

[ApiController]
public class ForumController : ControllerBase
{
    private readonly ForumService _forumService;
    

    public ForumController(ForumService forumService)
    {
        _forumService = forumService;
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Route("/GetFeed")]
    public IEnumerable<User> getUserFeed()
    {
        return _forumService.getUserFeed();
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Authorize]
    [Route("/profile")]
    public IEnumerable<Threads> profileThreadsBasedOnUserId()
    {
        var user = HttpContext.Items["User"] as User;
        
        return _forumService.getThreadsBasedOnUserId(user.Id);
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Authorize]
    [Route("/userprofile")]
    public User getUserInformation()
    {
        var user = HttpContext.Items["User"] as User;

        return _forumService.getUserInformation(user.Id).FirstOrDefault();
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Authorize]
    [Route("/usercomments")]
    public IEnumerable<UserCommentCreate> getUserComments()
    {
        var user = HttpContext.Items["User"] as User;

        return _forumService.getUserComments(user.Id);
    }
    
}