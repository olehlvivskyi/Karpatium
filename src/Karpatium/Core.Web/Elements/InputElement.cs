namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an input HTML element, such as &lt;input&gt;, &lt;textarea&gt;, etc.
/// </summary>
public class InputElement : Element
{
    /// <summary>
    /// Sets the specified text, replacing any existing value.
    /// </summary>
    /// <param name="text">The text to set as the value of the input HTML element.</param>
    public void SetText(string text)
    {
        WebElementWrapper.Clear();
        WebElementWrapper.SendKeys(text);
    }
}