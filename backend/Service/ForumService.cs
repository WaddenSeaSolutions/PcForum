using backend.DAL;
using backend.Model;
using DevOne.Security.Cryptography.BCrypt;

namespace backend.Service;

public class ForumService
{
    private readonly ForumDAL _forumDal;

    public ForumService(ForumDAL forumDal)
    {
        _forumDal = forumDal;
    }

    public User Register(User user)
    {
        string password = user.Password;
        string salt = BCryptHelper.GenerateSalt();
        string hashedPassword = BCryptHelper.HashPassword(password, salt);
        
        Console.WriteLine(hashedPassword);
        Console.WriteLine(salt);
        //return _forumDal.Register(user);
        return null;
    }

    public void DeleteUser(int id)
    {
        _forumDal.DeleteUser(id);
    }
    
    public IEnumerable<User> getUserFeed()
    {
        return _forumDal.GetUserFeed();
    }
    
}