namespace CrudCrud.API.Models;

[Serializable]
public sealed class UserRequest
{
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public int Age { get; set; }
    
    public bool IsActive { get; set; }
}