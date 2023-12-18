using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class Topic
{
    public int id { get; set; }
    
    [Required]
    [MinLength(1)]
    [StringLength(60)]
    public string title { get; set; }
    public Boolean deleted { get; set; }
    
    [Required]
    [StringLength(255)]
    public string image { get; set; }
}