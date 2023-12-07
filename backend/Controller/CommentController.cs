using backend.DAL;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
    [EnableRateLimiting("get")]
    [Route("/getComment/{threadId}")]
    public IEnumerable<UserComment> getCommentForThreads([FromRoute] int threadId)
    {
        return _commentService.getCommentForThreads(threadId);
    }
    
    
    [HttpPost]
    [EnableRateLimiting("comment")]
    [Authorize]
    [Route("/comment/{threadId}")]
    public void CreateComment([FromBody] UserComment userComment, int threadId)
    {
        var user = HttpContext.Items["User"] as User;

        userComment.threadId = threadId;

        userComment.deleted = false;

        userComment.userId = user.Id;
        
        userComment.utcTime = DateTime.UtcNow;
        
        _commentService.createComment(userComment);
    }



}