using Karpatium.Core.Attributes;

namespace Karpatium.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="string"/> to enhance its functionality.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Retrieves the enum value of type <typeparamref name="T"/> that corresponds to the string representation
    /// provided via the <see cref="StringValueAttribute"/> associated with the enum fields.
    /// </summary>
    /// <typeparam name="T">The enum type to retrieve the value from. Must be of type <see cref="Enum"/>.</typeparam>
    /// <param name="value">The string value to match against the <see cref="StringValueAttribute"/> of the enum fields.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided string value does not match any <see cref="StringValueAttribute"/>
    /// associated with the fields of the enum type <typeparamref name="T"/>.
    /// </exception>
    public static T GetEnumValue<T>(this string value) where T : Enum
    {
        var type = typeof(T);
        foreach (var fieldInfo in type.GetFields())
        {
            if (fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) is StringValueAttribute[] { Length: > 0 } attributes && attributes[0].StringValue == value)
            {
                return (T)fieldInfo.GetValue(null)!;
            }
        }

        throw new ArgumentException($"{nameof(StringExtensions)}: Value `{value}` is not valid for enum type `{type.Name}`.");
    }
}