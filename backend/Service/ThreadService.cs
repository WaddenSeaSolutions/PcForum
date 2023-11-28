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

    public void createThread(Threads threads)
    {
        _threadDal.createThread(threads);
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm)
    {
        return _threadDal.searchOnThreads(searchTerm);
    }

    public IEnumerable<Threads> getThreadDetails(int id)
    {
        return _threadDal.getThreadDetails(id);
    }
}