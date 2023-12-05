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

    public void createComment(Comment comment)
    {
        var sql = $@"INSERT INTO forum.comment
        (body, userId, utcTime, deleted, threadid)
        VALUES (@body, @userId, @utcTime, @deleted, @threadId);";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new
            {
                body = comment.body, userId = comment.userId, utcTime = comment.utcTime, comment.deleted, threadId = comment.threadId
            });
        }
    }
}