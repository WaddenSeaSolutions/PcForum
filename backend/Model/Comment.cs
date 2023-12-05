namespace backend.Model;

public class Comment
{
    public int id { get; set; }

    public string body { get; set; }
    public int ThreadId { get; set; }
    
    public int userId { get; set; }
    
    public DateTime utcTime { get; set; }
    
    public Boolean deleted { get; set; }
}