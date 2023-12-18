using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class ResponseThreadCreate
{
    public int id { get; set; }
    
    public string title { get; set; }
    
    public int topicId { get; set; }
    
    public string body { get; set; }
    
    public Boolean deleted { get; set;}
    
    public int userId { get; set; }
    
    public DateTime utcTime { get; set; }
}