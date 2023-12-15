using backend.DAL;
using backend.Model;

namespace backend.Service;

public class ThreadService
{

    private readonly ThreadDAL _threadDal;

    public ThreadService(ThreadDAL threadDal)
    {
        _threadDal = threadDal;
    }

    public IEnumerable<Threads> getThreadsForTopic(int topicId)
    {
        try
        {
            return _threadDal.getThreadsForTopic(topicId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to get threads for topic: " + e);
            throw new Exception("Could not get threads for topic " +topicId);
        }
    }

    public void createThread(ResponseThreadCreate rtc)
    {
        try
        {
            _threadDal.createThread(rtc);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not create thread" + e);
            throw new Exception("Could not create thread");
        }
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm, int topicId)
    {
        try
        {
            return _threadDal.searchOnThreads(searchTerm, topicId);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not searchOnThreads: "+ e);
            throw new Exception("Could not searchOnThreads");
        }
    }

    public Threads getThreadDetails(int id)
    {
        try
        {
            return _threadDal.getThreadDetails(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could get thread details: "+e);
            throw new Exception("Could not get thread details");
        }
    }

    public void deleteThread(int id)
    {
        try
        {
            _threadDal.deleteThread(id);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not delete thread: "+e);
            throw new Exception("Could not delete thread");
        }
    }
}