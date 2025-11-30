namespace Karpatium.Core.Extensions;

/// <summary>
/// Provides extension methods for the boolean type.
/// </summary>
public static class BooleanExtensions
{
    /// <summary>
    /// Converts a boolean value to an integer (true -> 1, false -> 0).
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>An integer representation: 1 (for true), 0 (for false).</returns>
    public static int ToInt(this bool value) => value ? 1 : 0;
}