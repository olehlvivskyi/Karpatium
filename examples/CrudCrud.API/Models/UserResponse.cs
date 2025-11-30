using System.Text.Json.Serialization;

namespace CrudCrud.API.Models;

[Serializable]
public sealed class UserResponse
{
    [JsonPropertyName("_id")]
    public required string Id { get; set; }
    
    public required string Username { get; set; }
    
    public required string Email { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public int Age { get; set; }
    
    public bool IsActive { get; set; }
}