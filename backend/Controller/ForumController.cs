using System.Security;
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
    public IActionResult Login(User user)
    {
        var userToAuthenticate = _forumService.Login(user);

        if (userToAuthenticate != null)
        {
            var token = _tokenService.CreateToken(userToAuthenticate);

            return Ok(token); // Assuming a successful login (200 OK)
        }

        return Unauthorized(); // Or any other appropriate status code for unsuccessful login
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

    [HttpPost]
    [Authorize]
    [Route("/TestValidationToken")]
    public IActionResult tokenTest()
    {
        var user = HttpContext.Items["User"] as User;
        if (user != null)
        {
            // Use the user object as needed
            return Ok();
        }
            // User was not found in HttpContext.Items. This might be due to improper token validation.
            return Unauthorized();
    }
    
}