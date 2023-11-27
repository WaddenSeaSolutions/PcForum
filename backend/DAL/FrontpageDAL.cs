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
            deleted as {nameof(Topic.deleted)},
            image as {nameof(Topic.image)}
            FROM forum.topics 
            WHERE deleted = false;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Topic>(sql);
        }
    }

    public void createTopic(Topic topic)
    {
        var sql = @"INSERT INTO forum.topics (title, deleted, image) VALUES (@title, @deleted, @image);";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { title = topic.title, deleted = topic.deleted, image = topic.image });
        }
    }

    public void deleteTopic(int id)
    {
        var sql = @"UPDATE forum.topics SET deleted = true WHERE id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { id });
        }
    }

    public Topic getTopicForUpdate(int id)
    {
        var sql = $@"SELECT id as {nameof(Topic.id)},
            title as {nameof(Topic.title)},
            deleted as {nameof(Topic.deleted)},
            image as {nameof(Topic.image)}
            FROM forum.topics WHERE id = {id};";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<Topic>(sql);
        }
    }

    public void updateTopic(int id, string title, string image)
    {
        var sql = $@"
        UPDATE forum.topics
        SET title = @title, image = @image
        WHERE id = @id;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { id, title, image });
        }
    }
}