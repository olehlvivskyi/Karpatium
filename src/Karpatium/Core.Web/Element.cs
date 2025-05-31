using System.Text.RegularExpressions;
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
    internal IWebElement WebElementWrapper
    {
        get
        {
            IWebElement element = MultipleWrapper ?? (Parent == null
                ? ConditionalWaiter.ForResult(() => WebManager.BrowserWrapper.FindElement(Selector!), 
                    $"{nameof(WebElementWrapper)} from global DOM failed.")
                : ConditionalWaiter.ForResult(() => Parent.FindElement(Selector!), 
                    $"{nameof(WebElementWrapper)} from parent element failed."));
            
            if (WebManager.IsDemoModeEnabled)
            {
                ApplyDemoMode(element);
            }
            
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
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }

    /// <summary>
    /// Simulates a double-click action on the element.
    /// </summary>
    public void DoubleClick()
    {
        if (WebManager.CurrentBrowserType == BrowserType.Safari)
        {
            ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.ExecuteJavascript("arguments[0].dispatchEvent(new MouseEvent('dblclick', { 'bubbles': true }));", WebElementWrapper), $"{nameof(DoubleClick)} failed.");
        }
        else
        {
            ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.DoubleClick(WebElementWrapper).Perform(), $"{nameof(DoubleClick)} failed.");
        }
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }

    /// <summary>
    /// Performs a drag-and-drop action by dragging the current element to another specified element.
    /// </summary>
    /// <param name="toElement">The target element where the current element will be dropped.</param>
    public void DragAndDrop(Element toElement)
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.DragAndDrop(WebElementWrapper, toElement.WebElementWrapper).Perform(), $"{nameof(DragAndDrop)} failed.");
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
    /// Retrieves the list of class names assigned to the element.
    /// </summary>
    public IReadOnlyList<string> GetClassNames()
    {
        return GetAttribute("class")?
                   .Split(' ')
                   .ToList() 
               ?? new List<string>();
    }

    /// <summary>
    /// Retrieves the CSS value of the specified property from the element.
    /// </summary>
    /// <param name="property">The name of the CSS property whose value is to be retrieved.</param>
    public string GetCssValue(string property)
    {
        return ConditionalWaiter.ForResult(() => WebElementWrapper.GetCssValue(property), $"{nameof(GetCssValue)} failed.");
    }

    /// <summary>
    /// Simulates hovering the mouse cursor over the element.
    /// </summary>
    public void HoverOver()
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.MoveToElement(WebElementWrapper).Perform(), $"{nameof(HoverOver)} failed.");
    }

    /// <summary>
    /// Simulates a right-click action (context click) on the element.
    /// </summary>
    public void RightClick()
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.ContextClick(WebElementWrapper).Perform(), $"{nameof(RightClick)} failed.");
    }

    /// <summary>
    /// Scrolls the element into the visible area of the browser window.
    /// </summary>
    public void ScrollTo()
    {
        ConditionalWaiter.ForNoException(() => WebManager.BrowserWrapper.Actions.ScrollToElement(WebElementWrapper).Perform(), $"{nameof(ScrollTo)} failed.");
    }

    /// <summary>
    /// If the element exists, returns a string representation of the element.
    /// </summary>
    public override string ToString()
    {
        return Exists
            ? Regex.Match(GetAttribute("outerHTML")!, @"<(\w+)\s+[^>]*>").Value
            : "NoElementFound";
    }

    private void ApplyDemoMode(IWebElement element)
    {
        const string demoAddJavaScriptStyle = "arguments[0].style.outline='3px solid rgba(255, 0, 0, 0.7)'; arguments[0].style.outlineOffset='-3px'; arguments[0].style.boxShadow='0 0 10px rgba(255, 0, 0, 0.7)';";
        const string demoRemoveJavaScriptStyle = "arguments[0].style.outline=''; arguments[0].style.outlineOffset=''; arguments[0].style.boxShadow='';";

        ConditionalWaiter.ForNoException(
            () =>
            {
                WebManager.Browser.ExecuteJavascript(demoAddJavaScriptStyle, element);
                Thread.Sleep(WebManager.DemoModeDelayInMilliseconds);
                WebManager.Browser.ExecuteJavascript(demoRemoveJavaScriptStyle, element);
                
            },
            "Element: Waiting for slow test and apply highlighting.");
    }
}