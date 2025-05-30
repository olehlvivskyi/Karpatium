using Karpatium.Core.Utilities;
using OpenQA.Selenium;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents an input HTML element with "text" type or textarea HTML element.
/// </summary>
public sealed class InputElement : Element
{
    /// <summary>
    /// Gets the "value" attribute of the input or textarea HTML element.
    /// </summary>
    public string? Value => GetAttribute("value");
    
    /// <summary>
    /// Appends the specified text to the current value without clearing it.
    /// </summary>
    /// <param name="text">The text to append to the existing value.</param>
    public void AppendText(string text)
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.SendKeys(text), $"{nameof(InputElement)}: {nameof(AppendText)} failed.");
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
    
    /// <summary>
    /// Clears the current text or value.
    /// </summary>
    public void Clear()
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.Clear(), $"{nameof(InputElement)}: {nameof(Clear)} failed.");
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }

    /// <summary>
    /// Clears the input field by simulating repeated backspace key presses until the field is empty.
    /// </summary>
    public void ClearUsingBackspace()
    {
        ConditionalWaiter.ForNoException(() => { while (Value != string.Empty) { WebElementWrapper.SendKeys(Keys.Backspace); } }, $"{nameof(InputElement)}: {nameof(ClearUsingBackspace)} failed.");
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
    
    /// <summary>
    /// Sets the specified text, replacing any existing value.
    /// </summary>
    /// <param name="text">The text to set as the value.</param>
    public void SetText(string text)
    {
        Clear();
        AppendText(text);
    }
}