using backend.DAL;
using backend.Model;

namespace backend.Service;

public class UserService
{
    private UserDal _userDal;

    public UserService(UserDal userDal)
    {
        _userDal = userDal;
    }


    public bool checkIfUsernameExist(string username)
    {
        try
        {
            return _userDal.checkIfUsernameExist(username);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to check if username exist: "+e.Message);
            throw new Exception("Failed to check if username exist");
        }
    }

    public bool checkIfEmailExist(string email)
    {
        try
        {
            return _userDal.checkIfEmailExist(email);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to check if email exist: "+ e.Message);
            throw new Exception("Failed to check if email exist");
        }
    }

    public void banUser(string username)
    {
        try
        {
            _userDal.banUser(username);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to ban user: " + e);
            throw new Exception("Could not ban user");
        }
    }

    public User login(UserLogin userToBeLoggedIn)
    {
        try
        {
            var userToCheck = _userDal.login(userToBeLoggedIn);
            if (BCrypt.Net.BCrypt.Verify(userToBeLoggedIn.Password, userToCheck.Password))
            {
                return userToCheck;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred during login" + e.Message);
            throw new Exception("Login failed");
        }
        return null;
    }

    /*
     * Encrypts the password with a workFactor of 15.
     * WorkFactor slows the encryption ensuring brute-forcing takes longer amounts of time
     */
    public bool register(User user) 
    {
        try
        {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);

        //Replaces existing password with an encrypted created by Bcrypt
        user.Password = hashedPassword;
        
        return _userDal.register(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception( "Failed to register");
        }
        
    }
}