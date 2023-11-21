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
    
    public bool isUserDeleted(string nameClaimValue)
    {
        var sql = $@"
        SELECT deleted
        FROM forum.users
        WHERE username = @Username;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<bool>(sql, new {Username = nameClaimValue});
        }
    }
}