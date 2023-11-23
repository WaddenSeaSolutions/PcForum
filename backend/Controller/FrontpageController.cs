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

    [HttpPut]
    [Route("/topics/{id}")]
    public void deleteTopic([FromRoute] int id)
    {
        _frontpageService.deleteTopic(id);
    }
    
    [HttpGet]
    [Route("/topic-update/{id}")]
    public Topic getTopicForUpdate([FromRoute] int id)
    {
        return _frontpageService.getTopicForUpdate(id);
        
    }

    [HttpPut]
    [Route("/topic-update/{id}")]
    public void updateTopic(int id, [FromBody] Topic topic)
    {
        _frontpageService.updateTopic(id, topic.title,topic.image);
    }
}