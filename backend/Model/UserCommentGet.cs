namespace backend.Model;

public class UserCommentGet
{
    public int id { get; set; }
    
    public string username { get; set; }

    public string body { get; set; }
    
    public int threadId { get; set; }
    
    public int userId { get; set; }
    
    public DateTime utctime { get; set; }
    
    public Boolean deleted { get; set; }
}