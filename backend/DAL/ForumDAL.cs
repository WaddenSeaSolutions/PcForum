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


    public User login(User user)

    {
        string loggedUsername = user.Username;
        var sql = $@"SELECT id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            password as {nameof(User.Password)},
            email as {nameof(User.Email)},
            userrole as {nameof(User.UserRole)},
            deleted as {nameof(User.Deleted)}
            FROM forum.users where username = @username";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new {username = user.Username, password = user.Password, email = user.Email, userrole = user.UserRole, deleted = user.Deleted});
        }
    }
}