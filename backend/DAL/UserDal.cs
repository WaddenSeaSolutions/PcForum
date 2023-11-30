using Npgsql;

namespace backend.DAL;

public class UserDal
{
    
    private readonly NpgsqlDataSource _dataSource;

    public UserDal(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    
    
}