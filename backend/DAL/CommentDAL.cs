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
        (body, userId, utcTime, deleted)
        VALUES (@body, @userId, @utcTime, @deleted);";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new
            {
                body = comment.body, userId = comment.userId, utcTime = comment.utcTime, comment.deleted
            });
        }
    }
}