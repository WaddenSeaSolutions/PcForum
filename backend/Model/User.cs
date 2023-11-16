namespace backend.Model;

public class User
{
    public int Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }
    
    public required string Email { get; set; }
    
    public required string Role  {get; set; }
    public Boolean Deleted { get; set; }
    
}