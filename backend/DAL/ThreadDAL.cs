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

    public IEnumerable<Threads> GetThreadsForTopic(int topicId)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
        title as {nameof(Threads.title)},
        topicId as {nameof(Threads.topicId)},
        body as {nameof(Threads.body)},
        likes as {nameof(Threads.likes)},
        deleted as {nameof(Threads.deleted)},
        userid as {nameof(Threads.userId)},
        utctime as {nameof(Threads.utcTime)}
        FROM forum.threads
        WHERE topicid = @topicId;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Threads>(sql, new {topicId = topicId});
        }
    }


    public void createThread(Threads threads)
    {
        var sql =
            $@"INSERT INTO forum.threads 
                           (title, topicid, body, likes, deleted, userid, utctime) 
                            VALUES (@title, @topicId, @body, @likes, @deleted, @userid, @utctime);";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql,
                new
                {
                    title = threads.title, topicId = threads.topicId, body = threads.body, likes = threads.likes,
                    deleted = threads.deleted, userid = threads.userId, utctime = threads.utcTime
                });
        }
    }

    public IEnumerable<Threads> searchOnThreads(string searchTerm)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
              title as {nameof(Threads.title)},
              topicId as {nameof(Threads.topicId)},
              body as {nameof(Threads.body)},
              likes as {nameof(Threads.likes)},
              deleted as {nameof(Threads.deleted)},
              userid as {nameof(Threads.userId)},
              utctime as {nameof(Threads.utcTime)}
              FROM forum.threads
              WHERE (LOWER(body) LIKE @searchTerm OR LOWER(title) LIKE @searchTerm)";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Threads>(sql, new
            {
                searchTerm = $"%{searchTerm.ToLower()}%"
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
        utctime as {nameof(Threads.utcTime)}
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