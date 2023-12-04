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
    public void createComment([FromBody] Comment comment)
    {
        _commentService.createComment(comment);
    }



}