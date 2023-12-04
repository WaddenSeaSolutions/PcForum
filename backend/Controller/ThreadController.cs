using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

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
    [Route("/threads/{topicId}")]
    public IEnumerable<Threads> GetThreadsForTopic([FromRoute]int topicId)
    {
        return _threadService.GetThreadsForTopic(topicId);
    }

    

    [HttpPost]
    [Authorize]
    [Route("/threads")]
    public void createThread([FromBody] ResponseThreadCreate rtc)
    {
        var user = HttpContext.Items["User"] as User;
        
        Console.WriteLine("halløj");
        rtc.utcTime = DateTime.UtcNow;
        rtc.userId = user.Id;
        rtc.deleted = false;
        
        _threadService.createThread(rtc);
    }

    [HttpGet]
    [Route("/searchOnThreads")]
    public IEnumerable<Threads> searchOnThreads([FromQuery] string searchTerm)
    {
        return _threadService.searchOnThreads(searchTerm);
    }

    [HttpGet]
    [Route("thread/{id}")]
    public Threads getThreadDetails([FromRoute] int id)
    {
        return _threadService.getThreadDetails(id);
    }
}