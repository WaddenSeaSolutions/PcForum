using backend.DAL;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

[ApiController]

public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }


    [HttpGet]
    [Route("/getComment/{threadId}")]
    public IEnumerable<UserCommentGet> getCommentForThreads([FromRoute] int threadId)
    {
        return _commentService.getCommentForThreads(threadId);
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("/comment/{threadId}")]
    public void CreateComment([FromBody] UserCommentCreate userCommentCreate, int threadId)
    {
        var user = HttpContext.Items["User"] as User;

        userCommentCreate.threadId = threadId;

        userCommentCreate.deleted = false;

        userCommentCreate.userId = user.Id;
        
        userCommentCreate.utcTime = DateTime.UtcNow;
        
        _commentService.createComment(userCommentCreate);
    }



}