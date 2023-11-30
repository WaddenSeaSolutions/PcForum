using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
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
    
}