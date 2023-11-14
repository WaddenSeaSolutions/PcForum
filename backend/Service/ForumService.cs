using backend.DAL;
using backend.Model;

namespace backend.Service;

public class ForumService
{
    private readonly ForumDAL _forumDal;

    public ForumService(ForumDAL forumDal)
    {
        _forumDal = forumDal;
    }

    /*
     * //Encrypts the password with a workFactor of 15.
     * WorkFactor slows the encryption ensuring brute-forcing takes longer amounts of time
     */
    public User Register(User user) 
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 15);

        //Replaces existing password with an encrypted created by Bcrypt
        user.Password = hashedPassword; 
        return user;
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