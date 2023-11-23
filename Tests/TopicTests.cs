

using System.Data;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;

namespace Tests;

public class Tests
{

    [TestCase("TestTitle", "TestImage", false)]
    public async Task topicCreationTest(string title, string image, Boolean deleted)
    {
        // Arrange
        var testTopic = new Topic()
        {
            deleted = deleted,
            title = title,
            image = image
        };

        // Act: Send POST request to create a topic
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        var json = await httpResponse.Content.ReadAsStringAsync();
        

        // Assert: Retrieve data from the database and compare
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = $@"SELECT id as {nameof(Topic.id)},
            title as {nameof(Topic.title)},
            image as {nameof(Topic.image)}
            from forum.topics
            ORDER BY id DESC;";

            var exp = conn.QueryFirstOrDefault<Topic>(query); // Use QueryFirstOrDefault instead of QueryFirst
            // Log the retrieved 'exp' for further inspection
            Console.WriteLine($"Retrieved topic from database: {JsonConvert.SerializeObject(exp)}");    


           

            // Compare the properties of topic and exp
            exp.Should().BeEquivalentTo(testTopic, options => options.Excluding(o => o.id));
        }
    }
    
    

}