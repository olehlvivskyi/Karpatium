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
    /// Closes the browser tab specified by its index.
    /// </summary>
    /// <param name="tabNumber">The index of the tab to close, where 1 is the first tab.</param>
    public void CloseTab(int tabNumber);

    /// <summary>
    /// Retrieves a value from the browser's local storage for the specified key or key fragment.
    /// </summary>
    /// <param name="keyPart">The key or part of the key used to locate the desired value in local storage.</param>
    /// <returns>The value associated with the specified key or null if the key does not exist.</returns>
    public object? GetLocalStorageValue(string keyPart);

    /// <summary>
    /// Executes the specified JavaScript code in the context of the currently selected frame or window.
    /// </summary>
    /// <param name="script">The JavaScript code to execute.</param>
    /// <param name="args">The arguments to be passed to the script, if any.</param>
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
    /// Opens a new browser tab and navigates to the specified URL if provided.
    /// </summary>
    /// <param name="url">The URL to navigate to in the new tab.</param>
    void OpenNewTab(string url = "");

    /// <summary>
    /// Reloads the current page in the browser.
    /// </summary>
    void ReloadPage();

    /// <summary>
    /// Resizes the browser window to the specified width and height.
    /// </summary>
    /// <param name="width">The desired width of the browser window, in pixels.</param>
    /// <param name="height">The desired height of the browser window, in pixels.</param>
    void ResizeWindow(int width, int height);

    /// <summary>
    /// Switches the browser's focus to a specified tab based on its index.
    /// </summary>
    /// <param name="tabNumber">The index of the tab to switch to, where 1 is the first tab.</param>
    public void SwitchToTab(int tabNumber);
}