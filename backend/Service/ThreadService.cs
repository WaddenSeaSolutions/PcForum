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

    public IEnumerable<Threads> getThreads(int id)
    {
        return _threadDal.getThreads(id);
    }

    public void createThread(Threads threads)
    {
        _threadDal.createThread(threads);
    }

}