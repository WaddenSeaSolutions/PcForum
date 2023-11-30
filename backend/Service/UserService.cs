using backend.DAL;

namespace backend.Service;

public class UserService
{
    private UserDal _userDal;

    public UserService(UserDal userDal)
    {
        _userDal = userDal;
    }

    
    

}