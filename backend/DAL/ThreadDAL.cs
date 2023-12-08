using backend.Model;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class ThreadDAL
{
    private readonly NpgsqlDataSource _dataSource;

    public ThreadDAL(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Threads> getThreadsForTopic(int topicId)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
        title as {nameof(Threads.title)},
        topicId as {nameof(Threads.topicId)},
        body as {nameof(Threads.body)},
        likes as {nameof(Threads.likes)},
        deleted as {nameof(Threads.deleted)},
        userid as {nameof(Threads.userId)},
        utctime as {nameof(Threads.utctime)}
        FROM forum.threads
        WHERE topicId = @topicId and deleted = false
        ORDER BY utctime DESC;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Threads>(sql, new {topicId = topicId});
        }
    }


    public void createThread(ResponseThreadCreate rtc)
    {
        var sqlCheck = "SELECT deleted from forum.topics where id = @topicId;";
        var sql =
            $@"INSERT INTO forum.threads 
                           (title, topicid, body, likes, deleted, userid, utctime) 
                            VALUES (@title, @topicId, @body, @likes, @deleted, @userid, @utctime);";

        using (var conn = _dataSource.OpenConnection())
        {
           var isDeleted = conn.ExecuteScalar<bool>(sqlCheck, new { topicId = rtc.topicId });
           if (isDeleted) throw new Exception("This topic is no longer amoung us");
           conn.Execute(sql,
                new
                {
                    title = rtc.title, topicId = rtc.topicId, body = rtc.body, likes = rtc.likes,
                    deleted = rtc.deleted, userid = rtc.userId, utctime = rtc.utcTime
                });
        }
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm, int topicId)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
              title as {nameof(Threads.title)},
              topicId as {nameof(Threads.topicId)},
              body as {nameof(Threads.body)},
              likes as {nameof(Threads.likes)},
              deleted as {nameof(Threads.deleted)},
              userid as {nameof(Threads.userId)},
              utctime as {nameof(Threads.utctime)}
              FROM forum.threads
              WHERE (LOWER(body) LIKE @searchTerm OR LOWER(title) LIKE @searchTerm) and deleted = false and topicid = @topicid
              ORDER BY utctime DESC;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Threads>(sql, new
            {
                searchTerm = $"%{searchTerm.ToLower()}%", topicid = topicId
            });
        }
    }

    public Threads getThreadDetails(int id)
    {
        var sql = $@"
        SELECT threads.id as {nameof(Threads.id)},
        title as {nameof(Threads.title)},
        topicId as {nameof(Threads.topicId)},
        body as {nameof(Threads.body)},
        likes as {nameof(Threads.likes)},
        threads.deleted as {nameof(Threads.deleted)},
        userid as {nameof(Threads.userId)},
        utctime as {nameof(Threads.utctime)},
        u.username as {nameof(Threads.username)}
        FROM forum.threads
        join forum.users u on u.id = threads.userid
        WHERE threads.id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Threads>(sql, new { id = id });
        }
    }
}