using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class Threads
{
    public int id { get; set; }
    
    [Required]
    [MinLength(1)]
    [StringLength(50)]
    public string title { get; set; }
    
    public int topicId { get; set; }
    
    [Required]
    [MinLength(1)]
    [StringLength(2000)]
    public string body { get; set; }
    
    public int likes { get; set; }
    
    public Boolean deleted { get; set;}
    
    public int userId { get; set; }
    
    public string username { get; set; }
    
    public DateTime utctime { get; set; }
}