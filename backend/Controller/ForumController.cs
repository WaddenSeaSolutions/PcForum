using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controller;

[ApiController]
public class ForumController : ControllerBase
{
    private readonly ForumService _forumService;

    private readonly EmailService _emailService;

    private readonly TokenService _tokenService;

    public ForumController(ForumService forumService, EmailService emailService, TokenService tokenService)
    {
        _forumService = forumService;
        _emailService = emailService;
        _tokenService = tokenService;
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
            return Ok(new { Message = "Registration successful" });
        }

        // Return a 400 Bad Request status code for failed registration
        return BadRequest("Registration failed");
    }

    [HttpPost]
    [Route("/login")]
    public IActionResult Login([FromBody] UserLogin userToBeLoggedIn)
    {
        User userToBeAuthenticated = _forumService.Login(userToBeLoggedIn);
        if (userToBeAuthenticated == null)
        {
            throw new Exception("Could not log in. User could not be authenticated.");
        }
        Console.WriteLine(userToBeAuthenticated.Email);
        var token = _tokenService.CreateToken(userToBeAuthenticated);

        return Ok(token); // Successful login (200 OK)
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

    [HttpGet]
    [Authorize]
    [Route("/profile")]
    public IEnumerable<Threads> profileThreadsBasedOnUserId()
    {
        var user = HttpContext.Items["User"] as User;
        
        return _forumService.getThreadsBasedOnUserId(user.Id);
    }

    [HttpGet]
    [Authorize]
    [Route("/userprofile")]
    public User getUserInformation()
    {
        var user = HttpContext.Items["User"] as User;

        return _forumService.getUserInformation(user.Id).FirstOrDefault();
    }

    [HttpGet]
    [Authorize]
    [Route("/usercomments")]
    public IEnumerable<UserComment> getUserComments()
    {
        var user = HttpContext.Items["User"] as User;

        return _forumService.getUserComments(user.Id);
    }
    
}