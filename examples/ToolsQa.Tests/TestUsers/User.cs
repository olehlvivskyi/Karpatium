namespace ToolsQa.Tests.TestUsers;

/// <summary>
/// Represents a user with authentication credentials.
/// </summary>
/// <remarks>
/// Provided as an example for data-based parallelization implementation.
/// </remarks>
[Serializable]
public sealed class User
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}