using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller;

[ApiController]
public class FrontpageController : ControllerBase
{
    private readonly FrontpageService _frontpageService;

    public FrontpageController(FrontpageService frontpageService)
    {
        _frontpageService = frontpageService;
    }

    [HttpGet]
    [Route("/topics")]
    public IEnumerable<Topic> getTopics()
    {
        return _frontpageService.getTopics();
        
    }

    [HttpPost]
    [Route("/topics")]
    public void createTopic([FromBody] Topic topic)
    {
        topic.deleted = false;
        _frontpageService.createTopic(topic);
    }
}