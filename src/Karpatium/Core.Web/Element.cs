using Karpatium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
    
    internal IReadOnlyList<IWebElement> FindElements(Selector selector) => WebElementWrapper.FindElements(selector.ByWrapper);

    /// <summary>
    /// Indicates whether the element exists in the DOM.
    /// </summary>
    public bool Exists
    {
        get
        {
            try
            {
                IWebElement element = MultipleWrapper ?? (Parent == null
                    ? WebManager.BrowserWrapper.FindElement(Selector!)
                    : Parent.FindElement(Selector!));
                return !string.IsNullOrEmpty(element.TagName);
            }
            catch
            {
                return false;
            }
        }
    }
    
    /// <summary>
    /// Gets a value indicating whether the element is enabled.
    /// </summary>
    public bool IsEnabled => ConditionalWaiter.ForResult(() => WebElementWrapper.Enabled, $"{nameof(IsEnabled)} failed.");

    /// <summary>
    /// Indicates whether the element is currently visible on the page.
    /// </summary>
    public bool IsVisible => ConditionalWaiter.ForResult(() => Exists && WebElementWrapper.Displayed, $"{nameof(IsVisible)} failed.");
    
    /// <summary>
    /// Simulates a click action on the element.
    /// </summary>
    public void Click()
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.Click(), $"{nameof(Click)} failed.");
    }

    /// <summary>
    /// Simulates a double-click action on the element.
    /// </summary>
    public void DoubleClick()
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.DoubleClick(WebElementWrapper).Perform(), 
            $"{nameof(DoubleClick)} failed.");
    }

    /// <summary>
    /// Gets the value of the specified attribute of the element.
    /// </summary>
    /// <param name="attribute">The name of the attribute to retrieve.</param>
    public string? GetAttribute(string attribute)
    {
        return ConditionalWaiter.ForResult(() => WebElementWrapper.GetAttribute(attribute), $"{nameof(GetAttribute)} failed.");
    }

    /// <summary>
    /// Retrieves the CSS value of the specified property from the element.
    /// </summary>
    /// <param name="property">The name of the CSS property whose value is to be retrieved.</param>
    /// <returns>The CSS value of the specified property as a string.</returns>
    public string GetCssValue(string property)
    {
        return ConditionalWaiter.ForResult(() => WebElementWrapper.GetCssValue(property), $"{nameof(GetCssValue)} failed.");
    }

    /// <summary>
    /// Simulates a right-click action (context click) on the element.
    /// </summary>
    public void RightClick()
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.ContextClick(WebElementWrapper).Perform(), 
            $"{nameof(RightClick)} failed.");
    }
}