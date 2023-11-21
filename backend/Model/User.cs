using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public  string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public string UserRole  {get; set; }
    public Boolean Deleted { get; set; }
    
}