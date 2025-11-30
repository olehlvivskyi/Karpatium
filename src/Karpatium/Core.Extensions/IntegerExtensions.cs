namespace Karpatium.Core.Extensions;

/// <summary>
/// Provides extension methods for the integer type.
/// </summary>
public static class IntegerExtensions
{
    /// <summary>
    /// Converts an integer to a boolean (1 -> true, 0 -> false).
    /// </summary>
    /// <param name="value">The integer value to convert.</param>
    /// <returns>A boolean representation: true (if 1), false (if 0).</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the integer is not 0 or 1.
    /// </exception>
    public static bool ToBool(this int value)
    {
        return value switch
        {
            1 => true,
            0 => false,
            _ => throw new InvalidOperationException("Integer value must be 0 or 1 to convert to a boolean."),
        };
    }
}