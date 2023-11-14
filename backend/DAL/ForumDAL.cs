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

    public User Register(User user)
    {
        var sql = $@"INSERT INTO forum.users (username, password)
            VALUES (@username, @password)
            RETURNING id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            password as {nameof(User.Password)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new  {username = user.Username, password = user.Password});
        }
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
                deleted as {nameof(User.Deleted)}
    FROM forum.users;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<User>(sql);
        }
    }

    
    

}