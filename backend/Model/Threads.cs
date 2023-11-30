namespace backend.Model;

public class Threads
{
    public int id { get; set; }
    
    public string title { get; set; }
    
    public int topicId { get; set; }
    
    public string body { get; set; }
    
    public int likes { get; set; }
    
    public Boolean deleted { get; set;}
    
    public int userId { get; set; }
    
    public string username { get; set; }
}