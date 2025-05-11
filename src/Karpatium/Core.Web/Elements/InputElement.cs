using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an input HTML element, such as &lt;input&gt;, &lt;textarea&gt;, etc.
/// </summary>
public class InputElement : Element
{
    /// <summary>
    /// Appends the specified text to the current value of the input HTML element without clearing it.
    /// </summary>
    /// <param name="text">The text to append to the existing value of the input HTML element.</param>
    public void AppendText(string text)
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.SendKeys(text), 
            "InputElement: Append Text failed.");
    }
    
    /// <summary>
    /// Clears the current text or value from the input HTML element.
    /// </summary>
    public void Clear()
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.Clear(), "InputElement: Clear failed.");
    }
    
    /// <summary>
    /// Sets the specified text, replacing any existing value.
    /// </summary>
    /// <param name="text">The text to set as the value of the input HTML element.</param>
    public void SetText(string text)
    {
        Clear();
        AppendText(text);
    }
}