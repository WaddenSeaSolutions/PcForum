using backend.Model;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class CommentDAL
{
    private readonly NpgsqlDataSource _dataSource;

    public CommentDAL(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public void createComment(UserComment userComment)
    {
        var sql = $@"INSERT INTO forum.comment
        (body, userId, utcTime, deleted, threadId)
        VALUES (@body, @userId, @utcTime, @deleted, @threadId);";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new
            {

                body = userComment.body, userId = userComment.userId, utcTime = userComment.utcTime, deleted = userComment.deleted, threadId = userComment.threadId
            });
        }
    }

    public IEnumerable<UserComment> getCommentForThreads(int threadId)
    {
        var sql = $@"SELECT id as {nameof(UserComment.id)},
        body as {nameof(UserComment.body)},
        userid as {nameof(UserComment.userId)},
        utctime as {nameof(UserComment.utcTime)},
        deleted as {nameof(UserComment.deleted)},
        threadId as {nameof(UserComment.threadId)}
        FROM forum.comment
        WHERE threadid = @threadId and deleted = false
        ";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<UserComment>(sql, new { threadId = threadId });
        }
    }
    
    
    
}