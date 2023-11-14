using backend.Model;
using Dapper;
using Npgsql;

namespace backend.DAL;

public class FrontpageDAL
{
    private readonly NpgsqlDataSource _dataSource;

    public FrontpageDAL(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Topic> getTopics()
    {
        var sql = $@"SELECT id as {nameof(Topic.id)},
            title as {nameof(Topic.title)},
            deleted as {nameof(Topic.deleted)}
            FROM forum.topics;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Topic>(sql);
        }
    }
}