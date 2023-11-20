

using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("TestTitle","TestImage")]
    public async Task topicCreationTest(string title, string image)
    {
        //Arrange
        var testTopic = new Topic()
        {
            id = 1,
            title = title,
            image = image
        };
        
        //Act
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "topics", testTopic);
        var json = await httpResponse.Content.ReadAsStringAsync();
        Topic topic = JsonConvert.DeserializeObject<Topic>(json);
        
        await using (var conn = Helper.DataSource.OpenConnection())
        {
            var query = $@"SELECT topic.id as {nameof(Topic.id)},
                title as {nameof(Topic.title)}";
        }

    }
}