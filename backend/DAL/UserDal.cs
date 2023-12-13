using backend.Model;
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

    public bool register(User user)
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