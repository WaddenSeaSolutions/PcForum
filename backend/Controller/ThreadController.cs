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
    [Route("/threads/{id}")]
    public IEnumerable<Threads> GetThreadsForTopic([FromRoute]int topicId)
    {
        return _threadService.GetThreadsForTopic(topicId);
    }

    

    [HttpPost]
    [Route("/threads")]
    public void createThread([FromBody] Threads threads)
    {
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
    public IEnumerable<Threads> getThreadDetails([FromRoute] int id)
    {
        return _threadService.getThreadDetails(id);
    }
}