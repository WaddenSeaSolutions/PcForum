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
    public void createThread([FromBody] Threads threads)
    {
        var user = HttpContext.Items["User"] as User;

        threads.utcTime = DateTime.UtcNow;
        threads.userId = user.Id;
        threads.deleted = false;
        
        _threadService.createThread(threads);
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