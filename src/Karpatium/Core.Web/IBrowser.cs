using Karpatium.Core.Web.BrowserParts;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
public interface IBrowser
{
    /// <summary>
    /// Provides access to advanced interactions within the browser instance.
    /// </summary>
    AdvancedInteractions AdvancedInteractions { get; }
    
    /// <summary>
    /// Provides access to JavaScript execution within the browser instance.
    /// </summary>
    JavaScript JavaScript { get; }
    
    /// <summary>
    /// Gets the current URL of the webpage loaded in the browser instance.
    /// </summary>
    /// <value>
    /// A string representing the URL of the current webpage.
    /// </value>
    string Url { get; }

    /// <summary>
    /// Maximizes the browser window to occupy the full screen.
    /// </summary>
    void MaximizeWindow();

    /// <summary>
    /// Navigates the browser to the specified URL.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    void NavigateTo(string url);
}