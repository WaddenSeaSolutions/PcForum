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

    public void createComment(Comment comment)
    {
        _commentDal.createComment(comment); 
    }

}