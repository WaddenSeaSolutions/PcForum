using backend.Service;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class UserDal
{
    
    private readonly NpgsqlDataSource _dataSource;

    public UserDal(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }


    public bool checkIfUsernameExist(string username)
    {
        string sql = "SELECT username FROM forum.users WHERE username = @username;";

        using (var conn = _dataSource.OpenConnection())
        {
            var usernameFromDb = conn.QueryFirstOrDefault<string>(sql, new { Username = username });
        
            if (usernameFromDb != null)
            {
                return true;
            }
        }

        return false;
    }

    public bool checkIfEmailExist(string email)
    {
        string sql = "SELECT email FROM forum.users WHERE email = @email";

        using (var conn = _dataSource.OpenConnection())
        {
            var emailFromDb = conn.QueryFirstOrDefault<string>(sql, new { Email = email });

            if (emailFromDb != null)
            {
                return true;
            }
        }

        return false;
    }

    public void banUser(string username)
    {
        
        string sql = $@"UPDATE forum.users SET deleted = true WHERE username = @username";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { username });
        }
    }
}