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

    public void createComment(UserCommentCreate userCommentCreate)
    {
        var sql = $@"INSERT INTO forum.comment
        (body, userId, utcTime, deleted, threadId)
        VALUES (@body, @userId, @utcTime, @deleted, @threadId);";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new
            {

                body = userCommentCreate.body, userId = userCommentCreate.userId, utcTime = userCommentCreate.utcTime, deleted = userCommentCreate.deleted, threadId = userCommentCreate.threadId
            });
        }
    }

    public IEnumerable<UserCommentGet> getCommentForThreads(int threadId)
    {
        var sql = $@"
    SELECT comment.id as {nameof(UserCommentGet.id)}, 
    comment.body as {nameof(UserCommentGet.body)}, 
    comment.userid as {nameof(UserCommentGet.userId)}, 
    comment.utctime as {nameof(UserCommentGet.utctime)}, 
    comment.deleted as {nameof(UserCommentGet.deleted)},
    comment.threadId as {nameof(UserCommentGet.threadId)}, 
    u.username as {nameof(UserCommentGet.username)} 
    FROM forum.comment 
    join forum.users u on u.id = comment.userid 
    WHERE comment.threadid = @threadId and comment.deleted = false 
    ";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<UserCommentGet>(sql, new { threadId = threadId });
        }
    }
    
    
    
}