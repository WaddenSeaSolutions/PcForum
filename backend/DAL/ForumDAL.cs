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

    public bool Register(User user)
    {
        var sql = $@"INSERT INTO forum.users (username, password, email, userrole, deleted)
        VALUES (@username, @password, @email, @userrole, @deleted);";

        using (var conn = _dataSource.OpenConnection())
        {
            var result = conn.Execute(sql, new { username = user.Username, password = user.Password, email = user.Email, userrole = user.UserRole, deleted = user.Deleted });

            if (result > 0)
                return true;
        }

        return false;
    }

    public void DeleteUser(int id)
    {
        var sql = $@"UPDATE forum.users
                  SET deleted = true
                  WHERE id = @id";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { id });
        }
    }

    public IEnumerable<User> GetUserFeed()
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


    public User login(UserLogin userToBeLoggedIn)
    {
        var sql = $@"SELECT id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            password as {nameof(User.Password)},
            email as {nameof(User.Email)},
            userrole as {nameof(User.UserRole)},
            deleted as {nameof(User.Deleted)}
            FROM forum.users where username = @username";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<User>(sql, new {username = userToBeLoggedIn.Username});
        }
    }

    public IEnumerable<Threads> getThreadsBasedOnUserId(int userId)
    {
        var sql = $@"SELECT id as {nameof(Threads.id)},
          title as {nameof(Threads.title)},
          topicId as {nameof(Threads.topicId)},
          body as {nameof(Threads.body)},
          likes as {nameof(Threads.likes)},
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