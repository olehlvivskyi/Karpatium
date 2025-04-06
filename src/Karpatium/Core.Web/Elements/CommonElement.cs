using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web.Elements;

/// <summary>
/// Represents a common HTML element, such as &lt;div&gt;, &lt;span&gt;, etc.
/// </summary>
public class CommonElement : Element
{
    /// <summary>
    /// Gets the text content of the associated web element.
    /// </summary>
    /// <remarks>
    /// This property retrieves the visible text or inner text of the HTML element represented by the current instance.
    /// </remarks>
    public string Text
    {
        get
        {
            var text = ConditionalWaiter.ForResult(() => WebElementWrapper.Text, "CommonElement: Text failed.");
            return text;
        }
    }
}