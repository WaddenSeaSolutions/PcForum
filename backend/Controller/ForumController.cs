using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

[ApiController]
public class ForumController : ControllerBase
{
    private readonly ForumService _forumService;

    private readonly EmailService _emailService;

    public ForumController(ForumService forumService, EmailService emailService)
    {
        _forumService = forumService;
        _emailService = emailService;
    }

    [HttpPost]
    [Route("/register")]
    public IActionResult Register(User user)
    {
        user.Deleted = false;
        user.UserRole = "standard";

        // Attempt to register the user in the database
        var isUserRegistered = _forumService.Register(user);

        if (isUserRegistered)
        {
            _emailService.SendEmail(user);
            // Return a 201 Created status code for successful registration
            return Created("/register", user);
        }

        // Return a 400 Bad Request status code for failed registration
        return BadRequest("Registration failed");
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