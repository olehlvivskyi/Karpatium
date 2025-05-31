using System.Drawing;
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
    /// Finds and returns the first web element that matches the specified selector.
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
    /// Retrieves the browser logs generated during the browsing session.
    /// </summary>
    /// <returns>A read-only collection of <see cref="LogEntry"/> containing the browser logs.</returns>
    internal IReadOnlyList<LogEntry> GetBrowserLogs() => DriverWrapper.Manage().Logs.GetLog(LogType.Browser);

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
    
    public string Title => DriverWrapper.Title;

    public string Url => DriverWrapper.Url;
    
    public void AcceptAlert() => DriverWrapper.SwitchTo().Alert().Accept();
    
    public void AddCookie(string name, string value)
    {
        Cookie cookie = new Cookie(name, value);
        DriverWrapper.Manage().Cookies.AddCookie(cookie);
    }

    public void CloseTab(int tabNumber)
    {
        string windowHandle = DriverWrapper.WindowHandles[tabNumber - 1];
        DriverWrapper.SwitchTo().Window(windowHandle);
        DriverWrapper.Close();
    }
    
    public void DeleteAllCookies() => DriverWrapper.Manage().Cookies.DeleteAllCookies();
    
    public void DeleteCookie(string cookieName) => DriverWrapper.Manage().Cookies.DeleteCookieNamed(cookieName);
    
    public void DismissAlert() => DriverWrapper.SwitchTo().Alert().Dismiss();

    public string? GetAlertText() => DriverWrapper.SwitchTo().Alert().Text;
    
    public IReadOnlyDictionary<string, string> GetAllCookies() => DriverWrapper.Manage().Cookies.AllCookies.ToDictionary(cookie => cookie.Name, cookie => cookie.Value);

    public Rectangle GetBounds() => new Rectangle(DriverWrapper.Manage().Window.Position, DriverWrapper.Manage().Window.Size);
    
    public object? GetLocalStorageValue(string keyPart)
    {
        if (JavaScriptExecutorWrapper.ExecuteScript("return window.localStorage;") is not Dictionary<string, object> items)
        {
            return null;
        }
        
        return items.FirstOrDefault(tempItem => tempItem.Key.Contains(keyPart)).Value;
    }

    public object? ExecuteJavascript(string script, params object[] args)
    {
        object? result = JavaScriptExecutorWrapper.ExecuteScript(script, args);
        return result;
    }
    
    public void FullScreenWindow() => DriverWrapper.Manage().Window.FullScreen();

    public void MaximizeWindow() => DriverWrapper.Manage().Window.Maximize();
    
    public void MinimizeWindow() => DriverWrapper.Manage().Window.Minimize();
    
    public void NavigateBack() => DriverWrapper.Navigate().Back();
    
    public void NavigateForward() => DriverWrapper.Navigate().Forward();
    
    public void NavigateTo(string url)
    {
        if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("The provided URL is invalid.", nameof(url));
        }
        
        Log.Verbose("{ClassName}: Navigating to `{Url}` url.", nameof(Browser), url);
        
        DriverWrapper.Navigate().GoToUrl(url);
    }

    public void OpenNewTab(string url = "")
    {
        DriverWrapper.SwitchTo().NewWindow(WindowType.Tab);
        if (!string.IsNullOrWhiteSpace(url))
        {
            NavigateTo(url);
        }
    }

    public void ReloadPage()
    {
        DriverWrapper.Navigate().Refresh();
    }

    public void ResizeWindow(int width, int height)
    {
        DriverWrapper.Manage().Window.Size = new Size(width, height);
    }
    
    public void SetAlertText(string text) => DriverWrapper.SwitchTo().Alert().SendKeys(text);

    public void SwitchToTab(int tabNumber)
    {
        DriverWrapper.SwitchTo().Window(DriverWrapper.WindowHandles[tabNumber - 1]);
    }
}