using backend.Attributes;
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
    public IEnumerable<UserCommentGet> getCommentForThreads([FromRoute] int threadId)
    {
        return _commentService.getCommentForThreads(threadId);
    }
    
    
    [HttpPost]
    [EnableRateLimiting("comment")]
    [Authorize]
    [Route("/comment/{threadId}")]
    public void createComment([FromBody] UserCommentCreate userCommentCreate, int threadId)
    {
        var user = HttpContext.Items["User"] as User;

        userCommentCreate.threadId = threadId;

        userCommentCreate.deleted = false;

        userCommentCreate.userId = user.Id;
        
        userCommentCreate.utcTime = DateTime.UtcNow;
        
        _commentService.createComment(userCommentCreate);
    }

    [HttpPut]
    [AuthorizeAdmin]
    [Route("/comment/{id}")]
    public void deleteComment([FromRoute] int id)
    {
        _commentService.deleteComment(id);
    }



}