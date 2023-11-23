using backend.Model;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class TokenDal
{
    
    private readonly NpgsqlDataSource _dataSource;


    public TokenDal(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public User userFromUsername(string nameClaimValue)
    {
        var sql = $@"
            SELECT id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            password as {nameof(User.Password)},
            email as {nameof(User.Email)},
            userrole as {nameof(User.UserRole)},
            deleted as {nameof(User.Deleted)}
            FROM forum.users
            WHERE username = @Username;";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new {Username = nameClaimValue});
        }
    }
}