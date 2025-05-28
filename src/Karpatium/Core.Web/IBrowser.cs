namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
public interface IBrowser
{
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
    void NavigateTo(string url);
}