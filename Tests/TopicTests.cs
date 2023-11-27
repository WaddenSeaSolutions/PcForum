

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

            var exp = conn.QueryFirstOrDefault<Topic>(query); 
            Console.WriteLine($"Retrieved topic from database: {JsonConvert.SerializeObject(exp)}");    


           

            // Compare the properties of topic and exp
            exp.Should().BeEquivalentTo(testTopic, options => options.Excluding(o => o.id));
        }
    }
    
    [Test] 
    public async Task CreateThenDeleteTopicTest() 
    { 
        var testTopic = new Topic()
        {
            deleted = false,
            title = "TestTitle",
            image = "TestImage"
        };

        // HttpClient for API requests
        var client = new HttpClient();

        // API POST request to create a new topic
        var httpResponse = await client.PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        httpResponse.EnsureSuccessStatusCode();  

// Separate query to fetch the topic with the highest ID
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = @"SELECT id FROM forum.topics ORDER BY id DESC LIMIT 1;";
            var createdTopicId = conn.QueryFirstOrDefault<int>(query); 

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody); 
            var createdTopic = JsonConvert.DeserializeObject<Topic>(responseBody);
            


            // API PUT request to delete the topic
            httpResponse = await client.PutAsync(Helper.ApiBaseUrl + "topics/" + createdTopicId, null);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"Request failed with status code {httpResponse.StatusCode}. Response: {errorResponse}");
            }

            httpResponse.EnsureSuccessStatusCode();

            // Assert: Retrieve the deleted flag from the database and compare
            
                var query2 = $@"SELECT deleted as {nameof(Topic.deleted)}
        FROM forum.topics
        WHERE id = @id;";

                var deleted = conn.QueryFirstOrDefault<bool>(query2, new { id = createdTopicId });

                Assert.IsTrue(deleted);
        }
    }
    
    

}