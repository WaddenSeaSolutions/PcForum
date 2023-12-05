using System.Net.Http.Headers;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;

namespace Tests;

public class ThreadTests
{
    [TestCase("TestTitle", "TestBody")]
    public async Task threadCreationTest(string title, string body)
    {
        var envVarKeyName = "tokenfortests";
        var token = Environment.GetEnvironmentVariable(envVarKeyName);


        Console.WriteLine(token);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Arrange
        int topicId = 1;
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = $@"SELECT id FROM forum.topics WHERE deleted = false ORDER BY id DESC";
            int? result = (await conn.QueryFirstOrDefaultAsync<int?>(query));
            if (result != null)
            {
                topicId = result.Value;
            }
        }
        var testThread = new Thread()
        {
            title = title,
            body = body,
            topicId = topicId
        };
        
        //Act
        await client.PostAsJsonAsync(Helper.ApiBaseUrl + "threads", testThread);

        // Assert
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = $@"SELECT id as {nameof(Thread.id)},
            title as {nameof(Thread.title)},
            body as {nameof(Thread.body)},
            topicid as {nameof(Thread.topicId)}
            from forum.threads WHERE topicid = @topicId ORDER BY id DESC";

            var exp = conn.QueryFirstOrDefault<Thread>(query);
            Console.WriteLine($"Retrieved topic from database: {JsonConvert.SerializeObject(exp)}"); 
            
            exp.Should().BeEquivalentTo(testThread, options => options.Excluding(o => o.id));
        }
    }
}