using backend.Model;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class ForumDAL
{

    private readonly NpgsqlDataSource _dataSource;

    public ForumDAL(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    

    public IEnumerable<User> getUserFeed()
    {
        var sql = $@"SELECT id as {nameof(User.Id)},
                 username as {nameof(User.Username)},
                password as {nameof(User.Password)},
                email as {nameof(User.Email)},
                userrole as {nameof(User.UserRole)},
                deleted as {nameof(User.Deleted)}
                FROM forum.users;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<User>(sql);
        }
    }

    public IEnumerable<Threads> getThreadsBasedOnUserId(int userId)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
          title as {nameof(Threads.title)},
          topicId as {nameof(Threads.topicId)},
          body as {nameof(Threads.body)},
          deleted as {nameof(Threads.deleted)}
          FROM forum.threads
          WHERE userid = @userid";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Threads>(sql, new { userid = userId });
        }
    }

    public IEnumerable<User> getUserInformation(int id)
    {
        var sql = $@"SELECT id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            email as {nameof(User.Email)}
            FROM forum.users
            WHERE id = @id";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<User>(sql, new { id = id });
        }
    }

    public IEnumerable<UserCommentCreate> getUserComments(int userId)
    {
        var sql = $@"SELECT id as {nameof(UserCommentCreate.id)},
        body as {nameof(UserCommentCreate.body)},
        userid as {nameof(UserCommentCreate.userId)},
        utctime as {nameof(UserCommentCreate.utcTime)},
        deleted as {nameof(UserCommentCreate.deleted)}
        FROM forum.comment WHERE userid = @userid AND deleted = false";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<UserCommentCreate>(sql, new { userid = userId });
        }
    }
}