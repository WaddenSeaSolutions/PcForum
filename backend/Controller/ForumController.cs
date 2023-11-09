using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

[ApiController]
public class ForumController : ControllerBase
{
    private readonly ForumService _forumService;

    public ForumController(ForumService forumService)
    {
        _forumService = forumService;
    }

    [HttpPost]
    [Route("/register")]
    public User Register([FromBody] User user)
    {
        return _forumService.Register(user);
    }
    
    
}