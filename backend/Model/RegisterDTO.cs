using System.ComponentModel.DataAnnotations;

namespace backend.Model;

public class RegisterDTO
{
    [Required]
    [MinLength(5)]
    [StringLength(20)]
    public  string Username { get; set; }
    
    [Required]
    [MinLength(8)]
    [StringLength(50)]
    public string Password { get; set; }
    
    [Required]
    [MinLength(5)]
    [StringLength(30)]
    [EmailAddress]
    public string Email { get; set; }
}