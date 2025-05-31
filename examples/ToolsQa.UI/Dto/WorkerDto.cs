namespace ToolsQa.UI.Dto;

/// <summary>
/// Represents a Data Transfer Object (DTO) for a worker.
/// </summary>
public sealed record WorkerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Age { get; init; }
    public required string Salary { get; init; }
    public required string Department { get; init; }
}