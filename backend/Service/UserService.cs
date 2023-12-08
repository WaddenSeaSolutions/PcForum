using backend.DAL;

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
}