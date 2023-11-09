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
        var sql = $@"INSERT INTO forum.users (username, password, email)
            VALUES (@username, @password, @email)
            RETURNING id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            password as {nameof(User.Password)},
            email as {nameof(User.Email)};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new  {username = user.Username, password = user.Password, email = user.Email});
        }
    }
    

}