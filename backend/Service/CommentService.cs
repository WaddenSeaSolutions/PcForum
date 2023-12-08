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
        _commentDal.createComment(userCommentCreate); 
    }

    public IEnumerable<UserCommentGet> getCommentForThreads(int threadId)
    {
        return _commentDal.getCommentForThreads(threadId);
    }

    public void deleteComment(int id)
    {
        _commentDal.deleteComment(id);
    }
}