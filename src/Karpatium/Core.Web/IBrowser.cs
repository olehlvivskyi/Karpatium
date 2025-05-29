namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
public interface IBrowser
{
    /// <summary>
    /// Gets the source code of the current webpage loaded in the browser instance.
    /// </summary>
    /// <value>
    /// A string representing the HTML source of the current webpage.
    /// </value>
    public string PageSource { get; }
    
    /// <summary>
    /// Gets the current URL of the webpage loaded in the browser instance.
    /// </summary>
    /// <value>
    /// A string representing the URL of the current webpage.
    /// </value>
    string Url { get; }

    /// <summary>
    /// Executes the specified JavaScript code in the context of the currently selected frame or window.
    /// </summary>
    /// <param name="script">The JavaScript code to execute.</param>
    /// <param name="args">The arguments to be passed to the script, if any.</param>
    /// <returns>The result of the script execution, which may be null or an object depending on the script.</returns>
    public object? ExecuteJavascript(string script, params object[] args);

    /// <summary>
    /// Maximizes the browser window to occupy the full screen.
    /// </summary>
    void MaximizeWindow();

    /// <summary>
    /// Navigates the browser to the specified URL.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided URL is null, empty, or not a valid absolute URI.
    /// </exception>
    void NavigateTo(string url);

    /// <summary>
    /// Switches the browser focus to the most recently opened browser tab or window.
    /// </summary>
    public void SwitchToChildTab();

    /// <summary>
    /// Switches the browser focus to the parent tab or the first opened browser window.
    /// </summary>
    public void SwitchToParentTab();
}