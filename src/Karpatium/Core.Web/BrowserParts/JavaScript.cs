using OpenQA.Selenium;

namespace Karpatium.Core.Web.BrowserParts;

/// <summary>
/// Provides access to JavaScript execution within the browser instance.
/// </summary>
public sealed class JavaScript
{
    private IWebDriver DriverWrapper { get; }
    private IJavaScriptExecutor JavaScriptExecutorWrapper => (IJavaScriptExecutor)DriverWrapper;
    
    internal JavaScript(IWebDriver driver)
    {
        DriverWrapper = driver;
    }

    /// <summary>
    /// Executes the specified JavaScript code in the context of the currently selected frame or window.
    /// </summary>
    /// <param name="script">The JavaScript code to execute.</param>
    /// <param name="args">The arguments to be passed to the script, if any.</param>
    /// <returns>The result of the script execution, which may be null or an object depending on the script.</returns>
    public object? Execute(string script, params object[] args)
    {
        var result = JavaScriptExecutorWrapper.ExecuteScript(script, args);
        return result;
    }
}