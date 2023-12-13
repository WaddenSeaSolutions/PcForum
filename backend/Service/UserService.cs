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
        return _userDal.checkIfUsernameExist(username);
    }

    public bool checkIfEmailExist(string email)
    {
        return _userDal.checkIfEmailExist(email);
    }

    public void banUser(string username)
    {
        _userDal.banUser(username);
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
        }
        return null;
    }

    /*
     * //Encrypts the password with a workFactor of 15.
     * WorkFactor slows the encryption ensuring brute-forcing takes longer amounts of time
     */
    public bool register(User user) 
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);

        //Replaces existing password with an encrypted created by Bcrypt
        user.Password = hashedPassword;
        
        return _userDal.register(user);
    }
}