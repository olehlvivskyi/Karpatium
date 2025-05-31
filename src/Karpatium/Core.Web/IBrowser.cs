using System.Drawing;
using System.Net;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a browser instance for managing test execution.
/// </summary>
public interface IBrowser
{
    /// <summary>
    /// Gets the source code of the current webpage loaded in the browser instance.
    /// </summary>
    public string PageSource { get; }

    /// <summary>
    /// Gets the title of the current webpage loaded in the browser instance.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the current URL of the webpage loaded in the browser instance.
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// Accepts the currently displayed alert on the browser.
    /// </summary>
    public void AcceptAlert();

    /// <summary>
    /// Adds a cookie to the browser session with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the cookie to add.</param>
    /// <param name="value">The value of the cookie to add.</param>
    public void AddCookie(string name, string value);

    /// <summary>
    /// Closes the browser tab specified by its index.
    /// </summary>
    /// <param name="tabNumber">The index of the tab to close, where 1 is the first tab.</param>
    public void CloseTab(int tabNumber);

    /// <summary>
    /// Deletes all cookies stored in the current browser session.
    /// </summary>
    public void DeleteAllCookies();

    /// <summary>
    /// Deletes a specific cookie in the current browser session by its name.
    /// </summary>
    /// <param name="cookieName">The name of the cookie to delete.</param>
    public void DeleteCookie(string cookieName);

    /// <summary>
    /// Dismisses the currently displayed alert on the browser, preventing any default action or confirmation associated with it.
    /// </summary>
    public void DismissAlert();

    /// <summary>
    /// Retrieves the text content of the currently displayed alert on the browser.
    /// </summary>
    public string? GetAlertText();

    /// <summary>
    /// Retrieves all cookies currently stored in the browser session.
    /// </summary>
    public IReadOnlyDictionary<string, string> GetAllCookies();

    /// <summary>
    /// Retrieves the bounds of the browser window, including its position and size.
    /// </summary>
    public Rectangle GetBounds();

    /// <summary>
    /// Retrieves a value from the browser's local storage for the specified key or key fragment.
    /// </summary>
    /// <param name="keyPart">The key or part of the key used to locate the desired value in local storage.</param>
    public object? GetLocalStorageValue(string keyPart);

    /// <summary>
    /// Executes the specified JavaScript code in the context of the currently selected frame or window.
    /// </summary>
    /// <param name="script">The JavaScript code to execute.</param>
    /// <param name="args">The arguments to be passed to the script, if any.</param>
    public object? ExecuteJavascript(string script, params object[] args);

    /// <summary>
    /// Switches the browser window to full-screen mode.
    /// </summary>
    public void FullScreenWindow();

    /// <summary>
    /// Minimizes the browser window to the taskbar.
    /// </summary>
    public void MinimizeWindow();

    /// <summary>
    /// Maximizes the browser window to occupy the full screen.
    /// </summary>
    public void MaximizeWindow();

    /// <summary>
    /// Navigates the browser back to the previous page in the browsing history.
    /// </summary>
    public void NavigateBack();

    /// <summary>
    /// Navigates the browser forward to the next page in the browsing history, if available.
    /// </summary>
    public void NavigateForward();

    /// <summary>
    /// Navigates the browser to the specified URL.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    public void NavigateTo(string url);

    /// <summary>
    /// Opens a new browser tab and navigates to the specified URL if provided.
    /// </summary>
    /// <param name="url">The URL to navigate to in the new tab.</param>
    public void OpenNewTab(string url = "");

    /// <summary>
    /// Reloads the current page in the browser.
    /// </summary>
    public void ReloadPage();

    /// <summary>
    /// Resizes the browser window to the specified width and height.
    /// </summary>
    /// <param name="width">The desired width of the browser window, in pixels.</param>
    /// <param name="height">The desired height of the browser window, in pixels.</param>
    public void ResizeWindow(int width, int height);

    /// <summary>
    /// Sets the specified text in the currently displayed alert on the browser.
    /// </summary>
    /// <param name="text">The text to input into the alert.</param>
    public void SetAlertText(string text);

    /// <summary>
    /// Switches the browser's focus to a specified tab based on its index.
    /// </summary>
    /// <param name="tabNumber">The index of the tab to switch to, where 1 is the first tab.</param>
    public void SwitchToTab(int tabNumber);
}