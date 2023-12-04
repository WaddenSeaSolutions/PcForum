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

    public IEnumerable<Threads> GetThreadsForTopic(int topicId)
    {
        return _threadDal.GetThreadsForTopic(topicId);
    }

    public void createThread(ResponseThreadCreate rtc)
    {
        _threadDal.createThread(rtc);
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm)
    {
        return _threadDal.searchOnThreads(searchTerm);
    }

    public Threads getThreadDetails(int id)
    {
        return _threadDal.getThreadDetails(id);
    }
}