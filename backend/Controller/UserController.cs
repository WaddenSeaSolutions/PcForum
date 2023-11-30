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
    
    
    
}