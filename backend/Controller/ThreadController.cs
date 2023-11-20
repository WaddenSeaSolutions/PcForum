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
    [Route("/threads")]
    public IEnumerable<Threads> getThreads(int id)
    {
        return _threadService.getThreads(id);
        
    }

    [HttpPost]
    [Route("/threads")]
    public void createThread([FromBody] Threads threads)
    {
        threads.deleted = false;
        _threadService.createThread(threads);
    }
    
}