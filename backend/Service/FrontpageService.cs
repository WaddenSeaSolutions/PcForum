using backend.DAL;
using backend.Model;

namespace backend.Service;

public class FrontpageService
{
    private readonly FrontpageDAL _frontpageDAL;

    public FrontpageService(FrontpageDAL frontpageDAL)
    {
        _frontpageDAL = frontpageDAL;
    }

    public IEnumerable<Topic> getTopics()
    {
        return _frontpageDAL.getTopics();
    }

    public void createTopic(Topic topic)
    {
        _frontpageDAL.createTopic(topic);
    }

    public void deleteTopic(int id)
    {
        _frontpageDAL.deleteTopic(id);
    }

    public Topic getTopicForUpdate(int id)
    {
        return _frontpageDAL.getTopicForUpdate(id);
    }

    public void updateTopic(int id, string title, string image)
    {
        _frontpageDAL.updateTopic(id, title, image);
    }
}