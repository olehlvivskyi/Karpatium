using Karpatium.Core.Utilities;
using OpenQA.Selenium;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a base class for HTML elements.
/// </summary>
public abstract class Element
{
    /// <summary>
    /// Represents the parent element of the current element, if one exists.
    /// </summary>
    internal Element? Parent { get; init; }
    
    /// <summary>
    /// Represents the selector used for locating UI elements on a web page.
    /// </summary>
    internal Selector? Selector { get; init; }

    /// <summary>
    /// Represents a wrapper for multiple web elements that can be associated with an instance of an element.
    /// </summary>
    internal IWebElement? MultipleWrapper { get; init; }

    /// <summary>
    /// Gets the underlying IWebElement associated with this element.
    /// <br />The resolution logic works as follows:
    /// <br />- If the element was found as part of a search for multiple elements, `MultipleWrapper` is returned.
    /// <br />- If the element has a parent (`Parent`), it is located from the parent's context
    /// using the defined `Selector`.
    /// <br />- If no parent exists, the element is searched in the global DOM using the `Selector`.
    /// <br />This property allows lazy resolution of the underlying web element, ensuring it is fetched
    /// as-needed and always reflects the current state of the page.
    /// </summary>
    /// <returns>The resolved <see cref="IWebElement"/> instance that represents this element.</returns>
    internal IWebElement WebElementWrapper
    {
        get
        {
            IWebElement element = MultipleWrapper ?? (Parent == null
                ? ConditionalWaiter.ForResult(() => WebManager.BrowserWrapper.FindElement(Selector!), 
                    $"{nameof(WebElementWrapper)} from global DOM failed.")
                : ConditionalWaiter.ForResult(() => Parent.FindElement(Selector!), 
                    $"{nameof(WebElementWrapper)} from parent element failed."));
            return element;
        }
    }

    private IWebElement FindElement(Selector selector) => WebElementWrapper.FindElement(selector.ByWrapper);
    
    internal IReadOnlyList<IWebElement> FindElements(Selector selector) 
        => WebElementWrapper.FindElements(selector.ByWrapper);

    /// <summary>
    /// Simulates a click action on the element.
    /// </summary>
    public void Click()
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.Click(), $"{nameof(Click)} failed.");
    }
}