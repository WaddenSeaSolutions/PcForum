

using System.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;

namespace Tests;

public class Tests
{
    

    [TestCase("TestTitle", "TestImage", false)]
    public async Task topicCreationTest(string title, string image, Boolean deleted)
    {
        
        var envVarKeyName = "tokenfortests";
        var token = Environment.GetEnvironmentVariable(envVarKeyName);
        

        
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Arrange
        var testTopic = new Topic()
        {
            deleted = deleted,
            title = title,
            image = image
        };

        // Act: Send POST request to create a topic
        var httpResponse = await client.PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        
        

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
    public async Task createThenDeleteTopicTest() 
    { 
        
        var envVarKeyName = "tokenfortests";
        var token = Environment.GetEnvironmentVariable(envVarKeyName);
        
        
        
        
        var testTopic = new Topic()
        {
            deleted = false,
            title = "TestTitle",
            image = "TestImage"
        };

        // HttpClient for API requests
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // API POST request to create a new topic
        var httpResponse = await client.PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        

// Separate query to fetch the topic with the highest ID
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = @"SELECT id FROM forum.topics ORDER BY id DESC LIMIT 1;";
            var createdTopicId = conn.QueryFirstOrDefault<int>(query); 

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            


            // API PUT request to delete the topic
            httpResponse = await client.PutAsync(Helper.ApiBaseUrl + "topics/" + createdTopicId, null);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"Request failed with status code {httpResponse.StatusCode}. Response: {errorResponse}");
            }

           

            // Assert: Retrieve the deleted flag from the database and compare
            
                var query2 = $@"SELECT deleted as {nameof(Topic.deleted)}
        FROM forum.topics
        WHERE id = @id;";

                var deleted = conn.QueryFirstOrDefault<bool>(query2, new { id = createdTopicId });

                Assert.IsTrue(deleted);
        }
    }

    [Test]
    public async Task createThenUpdateTopic()
    {
        var envVarKeyName = "tokenfortests";
        var token = Environment.GetEnvironmentVariable(envVarKeyName);
        

        
        var testTopic = new Topic()
        {
            deleted = false,
            title = "NotUpdatedTitle",
            image = "NotUpdatedImage"
        };

        // HttpClient for API requests
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // API POST request to create a new topic
        var httpResponse = await client.PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        
        
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = @"SELECT id FROM forum.topics ORDER BY id DESC LIMIT 1;";
            var createdTopicId = conn.QueryFirstOrDefault<int>(query); 

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody); 
            var createdTopic = JsonConvert.DeserializeObject<Topic>(responseBody);

            var updatedTopic = new Topic()
            {
                id = createdTopicId,
                title = "UpdatedTitle",
                image = "https://i.imgur.com/PNaYuFd.png"
            };

            var json = JsonConvert.SerializeObject(updatedTopic);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            // API PUT request to update the topic
            httpResponse = await client.PutAsync(Helper.ApiBaseUrl + "topic-update/" + createdTopicId, httpContent);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorResponse = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception($"Request failed with status code {httpResponse.StatusCode}. Response: {errorResponse}");
            }

            // Assert
            
            var query2 = $@"SELECT title as {nameof(Topic.title)},
            image as {nameof(Topic.image)},
            deleted as {nameof(Topic.deleted)},
            id as {nameof(Topic.id)}
        FROM forum.topics
        WHERE id = @id;";

            var updated = conn.QueryFirstOrDefault<Topic>(query2, new { id = createdTopicId });

            Assert.That(updated.title, Is.EqualTo(updatedTopic.title));
            Assert.That(updated.image, Is.EqualTo(updatedTopic.image));
        }
    }
    
    
    

}