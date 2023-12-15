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
        try
        {
            return _forumDal.getUserFeed();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get user feed: " + e.Message);
            throw new Exception("Could not get user feed");
        }
    }

    public IEnumerable<Threads> getThreadsBasedOnUserId(int userId)
    {
        try
        {
            return _forumDal.getThreadsBasedOnUserId(userId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get threads based on user id: " + e.Message);
            throw new Exception("Failed to get threads based on user id");
        }
    }

    public IEnumerable<User> getUserInformation(int id)
    {
        try
        {
            return _forumDal.getUserInformation(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get user information: " + e.Message);
            throw new Exception("Could not get user information");
        }
    }

    public IEnumerable<UserCommentCreate> getUserComments(int userId)
    {
        try
        {
            return _forumDal.getUserComments(userId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get user comments: " + e.Message);
            throw new Exception("Could not get user comments");
        }
    }
}