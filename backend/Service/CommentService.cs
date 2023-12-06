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

    public void createComment(UserComment userComment)
    {
        _commentDal.createComment(userComment); 
    }

    public IEnumerable<UserComment> getCommentForThreads(int threadId)
    {
        return _commentDal.getCommentForThreads(threadId);
    }

}