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
    
    
    public IEnumerable<User> getUserFeed()
    {
        return _forumDal.getUserFeed();
    }

    public IEnumerable<Threads> getThreadsBasedOnUserId(int userId)
    {
        return _forumDal.getThreadsBasedOnUserId(userId);
    }

    public IEnumerable<User> getUserInformation(int id)
    {
        return _forumDal.getUserInformation(id);
    }

    public IEnumerable<UserCommentCreate> getUserComments(int userId)
    {
        return _forumDal.getUserComments(userId);
    }
}