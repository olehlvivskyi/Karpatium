using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents a common HTML element, such as &lt;div&gt;, &lt;span&gt;, etc.
/// </summary>
public sealed class CommonElement : Element
{
    /// <summary>
    /// Gets the text content of the associated web element.
    /// </summary>
    public string Text
    {
        get
        {
            string text = ConditionalWaiter.ForResult(() => WebElementWrapper.Text, $"{nameof(CommonElement)}: {nameof(Text)} failed.");
            
            return text;
        }
    }
}