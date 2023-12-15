using backend.DAL;
using backend.Model;

namespace backend.Service;

public class CommentService
{
    private readonly CommentDAL _commentDal;

    public CommentService(CommentDAL commentDal)
    {
        _commentDal = commentDal;
    }

    public void createComment(UserCommentCreate userCommentCreate)
    {
        try
        {
            _commentDal.createComment(userCommentCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to create comment: " + e.Message);
            throw new Exception("Could not create comment");
        }
    }

    public IEnumerable<UserCommentGet> getCommentForThreads(int threadId)
    {
        try
        {
            return _commentDal.getCommentForThreads(threadId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get comment for threads: " + e);
            throw new Exception("Could not get comment for thread");
        }
    }

    public void deleteComment(int id)
    {
        try
        {
            _commentDal.deleteComment(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to delete comment: " + e);
            throw new Exception("Could not delete comment");
        }
    }
}