using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace backend.Controller;

[ApiController]


public class ThreadController : ControllerBase
{
    
    private readonly ThreadService _threadService;

    public ThreadController(ThreadService threadService)
    {
        _threadService = threadService;
    }
    
    [HttpGet]
    [EnableRateLimiting("get")]
    [Route("/threads/{topicId}")]
    public IEnumerable<Threads> getThreadsForTopic([FromRoute]int topicId)
    {
        return _threadService.getThreadsForTopic(topicId);
    }

    

    [HttpPost]
    [EnableRateLimiting("fixed")]
    [Authorize]
    [Route("/threads")]
    public void createThread([FromBody] ResponseThreadCreate rtc)
    {
        var user = HttpContext.Items["User"] as User;
        
        rtc.utcTime = DateTime.UtcNow;
        rtc.userId = user.Id;
        rtc.deleted = false;
        
        _threadService.createThread(rtc);
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Route("/searchOnThreads")]
    public IEnumerable<Threads> searchOnThreads([FromQuery] string searchTerm, [FromQuery] int topicId)
    {
        return _threadService.searchOnThreads(searchTerm, topicId);
    }

    [HttpGet]
    [EnableRateLimiting("get")]
    [Route("thread/{id}")]
    public Threads getThreadDetails([FromRoute] int id)
    {
        return _threadService.getThreadDetails(id);
    }
}