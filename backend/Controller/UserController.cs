using backend.Attributes;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace backend.Controller;

[ApiController]
public class UserController : ControllerBase
{
    private UserService _userService;
    
    private readonly TokenService _tokenService;
    
    private readonly EmailService _emailService;



    public UserController(UserService userService, TokenService tokenService, EmailService emailService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _emailService = emailService;

    }

    [HttpPost]
    [EnableRateLimiting("login")]
    [Route("/login")]
    public IActionResult login([FromBody] UserLogin userToBeLoggedIn)
    {
        User userToBeAuthenticated = _userService.login(userToBeLoggedIn);
        if (userToBeAuthenticated == null)
        {
            throw new Exception("Could not log in. User could not be authenticated.");
        }
        var token = _tokenService.createToken(userToBeAuthenticated);

        return Ok(token); // Successful login (200 OK)
    }
    
    [HttpPost]
    [EnableRateLimiting("register")]
    [Route("/register")]
    public IActionResult register(User user)
    {
        user.Deleted = false;
        user.UserRole = "standard";

        // Attempt to register the user in the database
        var isUserRegistered = _userService.register(user);

        if (isUserRegistered)
        {
            _emailService.sendEmail(user);
            // Return a 201 Created status code for successful registration
            return Ok(new { Message = "Registration successful" });
        }

        // Return a 400 Bad Request status code for failed registration
        return BadRequest("Registration failed");
    }
    
    
    /*
     * A method to check if a username is already in use
     */

    [HttpPost]
    [Route("/checkUsername,{username}")]
    public bool checkIfUsernameExist(string username)
    {
        bool isUserNameInUse = _userService.checkIfUsernameExist(username);
        if (isUserNameInUse) //Returns true if username is in use
            return true;

        return false;
    }

    [HttpPost]
    [Route("/checkEmail{email}")]
    public bool checkIfEmailExist(string email)
    {
        bool isEmailInUse = _userService.checkIfEmailExist(email);
        
        if (isEmailInUse) // Returns true if email is in use
            return true;

        return false;
    }

    [HttpPut]
    [AuthorizeAdmin]
    [Route("/user")]
    public void banUser([FromBody] UsernameModel model)
    {
        _userService.banUser(model.Username);
    }
    
}