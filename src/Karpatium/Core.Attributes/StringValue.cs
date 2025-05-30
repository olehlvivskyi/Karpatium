namespace Karpatium.Core.Attributes;

/// <summary>
/// An attribute used to assign a string value to an enum field.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class StringValueAttribute(string value) : Attribute
{
    /// <summary>
    /// Gets the string value associated with the attributed enum field.
    /// </summary>
    public string StringValue { get; } = value;
}