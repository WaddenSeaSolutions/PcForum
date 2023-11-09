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

    public User Register(User user)
    {
        return _forumDal.Register(user);
    }
    
    
}