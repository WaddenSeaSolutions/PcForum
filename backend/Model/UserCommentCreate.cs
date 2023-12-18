using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class UserCommentCreate
{
    public int id { get; set; }

    [Required]
    [MinLength(1)]
    [StringLength(2000)]
    public string body { get; set; }
    public int threadId { get; set; }
    
    public int userId { get; set; }
    
    public DateTime utcTime { get; set; }
    
    public Boolean deleted { get; set; }
}