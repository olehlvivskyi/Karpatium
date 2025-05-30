using System.Reflection;
using Karpatium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
internal sealed class Browser(IWebDriver driver) : IBrowser
{
    private IWebDriver DriverWrapper { get; } = driver;
    
    internal Actions Actions => new(DriverWrapper);
    private IJavaScriptExecutor JavaScriptExecutorWrapper => (IJavaScriptExecutor) DriverWrapper;

    /// <summary>
    /// Finds and returns the first web element that matches the specified sFelector.
    /// </summary>
    /// <param name="selector">The selector used to identify the web element.</param>
    /// <returns>The first <see cref="IWebElement"/> that matches the specified selector.</returns>
    internal IWebElement FindElement(Selector selector) => DriverWrapper.FindElement(selector.ByWrapper);

    /// <summary>
    /// Finds and returns a list of web elements that match the specified selector.
    /// </summary>
    /// <param name="selector">The selector used to identify the web elements.</param>
    /// <returns>A read-only list of <see cref="IWebElement"/> that match the specified selector.</returns>
    internal IReadOnlyList<IWebElement> FindElements(Selector selector) => DriverWrapper.FindElements(selector.ByWrapper);

    /// <summary>
    /// Captures a screenshot of the current browser window and returns it as a <see cref="Screenshot"/> object.
    /// </summary>
    /// <returns>A <see cref="Screenshot"/> object representing the current state of the browser window.</returns>
    internal Screenshot GetScreenshot() => ((ITakesScreenshot)DriverWrapper).GetScreenshot();

    /// <summary>
    /// Closes all browser windows and safely ends the WebDriver session.
    /// </summary>
    internal void Quit() => DriverWrapper.Quit();
    
    public string PageSource => DriverWrapper.PageSource;

    public string Url => DriverWrapper.Url;
    
    public object? ExecuteJavascript(string script, params object[] args)
    {
        var result = JavaScriptExecutorWrapper.ExecuteScript(script, args);
        return result;
    }

    public void MaximizeWindow() => DriverWrapper.Manage().Window.Maximize();
    
    public void NavigateTo(string url)
    {
        if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("The provided URL is invalid.", nameof(url));
        }
        
        Log.Verbose("{ClassName}: Navigating to `{Url}` url.", nameof(Browser), url);
        
        DriverWrapper.Navigate().GoToUrl(url);
    }

    public void SwitchToChildTab()
    {
        DriverWrapper.SwitchTo().Window(DriverWrapper.WindowHandles.Last());
    }

    public void SwitchToParentTab()
    {
        DriverWrapper.SwitchTo().Window(DriverWrapper.WindowHandles.First());
    }
}