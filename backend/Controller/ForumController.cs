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
    public User Register(User user)
    {
        user.Deleted = false;
        user.UserRole = "standard";
        return _forumService.Register(user);
    }

    [HttpPost]
    [Route("/login")]
    public User Login(User user)
    {
        return _forumService.Login(user);
    }

    [HttpPut]
    [Route("/DeleteUser{id}")]
    public void DeleteUser(int id)
    {
       _forumService.DeleteUser(id);
    }

    [HttpGet]
    [Route("/GetFeed")]
    public IEnumerable<User> getUserFeed()
    {
        return _forumService.getUserFeed();
    }
    
    
}