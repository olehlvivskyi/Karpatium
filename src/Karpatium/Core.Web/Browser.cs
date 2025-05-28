using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
internal sealed class Browser : IBrowser
{
    private IWebDriver DriverWrapper { get; }
    
    internal Actions Actions => new(DriverWrapper);
    private IJavaScriptExecutor JavaScriptExecutorWrapper => (IJavaScriptExecutor) DriverWrapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="Browser"/> class with the specified WebDriver.
    /// </summary>
    /// <param name="driver">The WebDriver instance to be wrapped and controlled by the browser.</param>
    internal Browser(IWebDriver driver)
    {
        DriverWrapper = driver;
    }

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
    /// Closes all browser windows and safely ends the WebDriver session.
    /// </summary>
    internal void Quit() => DriverWrapper.Quit();

    /// <summary>
    /// Gets the current URL of the webpage loaded in the browser instance.
    /// </summary>
    /// <value>
    /// A string representing the URL of the current webpage.
    /// </value>
    public string Url => DriverWrapper.Url;
    
    /// <summary>
    /// Executes the specified JavaScript code in the context of the currently selected frame or window.
    /// </summary>
    /// <param name="script">The JavaScript code to execute.</param>
    /// <param name="args">The arguments to be passed to the script, if any.</param>
    /// <returns>The result of the script execution, which may be null or an object depending on the script.</returns>
    public object? ExecuteJavascript(string script, params object[] args)
    {
        var result = JavaScriptExecutorWrapper.ExecuteScript(script, args);
        return result;
    }

    /// <summary>
    /// Maximizes the browser window to occupy the full screen.
    /// </summary>
    public void MaximizeWindow() => DriverWrapper.Manage().Window.Maximize();

    /// <summary>
    /// Navigates the browser to the specified URL.
    /// </summary>
    /// <param name="url">The URL to navigate to. Must be a well-formed absolute URI.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided URL is null, empty, or not a valid absolute URI.
    /// </exception>
    public void NavigateTo(string url)
    {
        if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("The provided URL is invalid.", nameof(url));
        }
        
        Log.Verbose("Browser: Navigating to `{Url}` url.", url);
        
        DriverWrapper.Navigate().GoToUrl(url);
    }
}