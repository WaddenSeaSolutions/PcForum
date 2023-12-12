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
        return _threadDal.getThreadsForTopic(topicId);
    }

    public void createThread(ResponseThreadCreate rtc)
    {
        _threadDal.createThread(rtc);
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm, int topicId)
    {
        return _threadDal.searchOnThreads(searchTerm, topicId);
    }

    public Threads getThreadDetails(int id)
    {
        return _threadDal.getThreadDetails(id);
    }

    public void deleteThread(int id)
    {
        _threadDal.deleteThread(id);
    }
}