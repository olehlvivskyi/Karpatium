using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Karpatium.Core.Web.BrowserParts;

/// <summary>
/// Provides access to advanced interactions within the browser instance.
/// </summary>
public sealed class AdvancedInteractions
{
    private IWebDriver DriverWrapper { get; }
    private Actions ActionsWrapper => new (DriverWrapper);

    internal AdvancedInteractions(IWebDriver driver)
    {
        DriverWrapper = driver;
    }

    /// <summary>
    /// Scrolls the browser viewport to bring the specified element into view.
    /// </summary>
    /// <param name="element">The element to scroll into view.</param>
    public void ScrollToElement(Element element)
    {
        var actions = ActionsWrapper;
        actions.ScrollToElement(element.WebElementWrapper);
        actions.Perform();
    }
}