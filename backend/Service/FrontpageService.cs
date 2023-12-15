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
        try
        {
            return _frontpageDAL.getTopics();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get topics: " + e.Message);
            throw new Exception("Could not get topics");
        }
    }

    public void createTopic(Topic topic)
    {
        try
        {
            _frontpageDAL.createTopic(topic);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to create a topic: " + e.Message);
            throw;
        }
    }

    public void deleteTopic(int id)
    {
        try
        {
            _frontpageDAL.deleteTopic(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to delete topic" + e.Message);
            throw new Exception("Could not delete topic");
        }
    }

    public Topic getTopicForUpdate(int id)
    {
        try
        {
            return _frontpageDAL.getTopicForUpdate(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get topic for update: " + e.Message);
            throw new Exception("Could not get topic for update");
        }
    }

    public void updateTopic(int id, string title, string image)
    {
        try
        {
            _frontpageDAL.updateTopic(id, title, image);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to update topic: " + e);
            throw new Exception("Could not update topic");
        }
    }
}