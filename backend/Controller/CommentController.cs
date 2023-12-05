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
    
    [HttpPost]
    [Authorize]
    [Route("/comment/{threadId}")]
    public void CreateComment([FromBody] Comment comment, int threadId)
    {
        var user = HttpContext.Items["User"] as User;

        comment.threadId = threadId;

        comment.deleted = false;

        comment.userId = user.Id;
        
        comment.utcTime = DateTime.UtcNow;
        
        _commentService.createComment(comment);
    }



}