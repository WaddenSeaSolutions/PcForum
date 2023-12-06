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
    public void createComment([FromBody] UserComment userComment, [FromRoute]int threadId)
    {
        userComment.deleted = false;
        userComment.ThreadId = threadId;
        _commentService.createComment(userComment);
    }

    [HttpGet]
    [Route("/getComment/{threadId}")]
    public IEnumerable<UserComment> getCommentForThreads([FromRoute] int threadId)
    {
        return _commentService.getCommentForThreads(threadId);
    }



}