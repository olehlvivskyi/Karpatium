using Karpatium.Core.Attributes;

namespace Karpatium.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Enum"/> to enhance its functionality.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the string value associated with the given enum field, specified by the <see cref="StringValueAttribute"/>.
    /// </summary>
    /// <param name="value">The target enum field whose associated string value is to be retrieved.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the enum field does not exist, or if the <see cref="StringValueAttribute"/> is not applied to the field.
    /// </exception>
    public static string GetStringValue(this Enum value)
    {
        var type = value.GetType();
        var fieldInfo = type.GetField(value.ToString());

        if (fieldInfo is null)
        {
            throw new InvalidOperationException($"{nameof(EnumExtensions)}: Field `{value.ToString()}` not found in `{type.FullName}`.");
        }

        if (fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) is not StringValueAttribute[] attributes || attributes.Length == 0)
        {
            throw new InvalidOperationException($"{nameof(EnumExtensions)}: No `{nameof(StringValueAttribute)}` found for field `{value.ToString()}` in `{type.FullName}`.");
        }

        return attributes[0].StringValue;
    }
}