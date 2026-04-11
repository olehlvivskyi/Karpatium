namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a mobile driver instance for managing test execution.
/// </summary>
public interface IMobile
{
    /// <summary>
    /// Gets the implicit wait timeout applied when searching for elements.
    /// </summary>
    public TimeSpan ImplicitWait { get; set; }

    /// <summary>
    /// Gets the page source of the current view.
    /// </summary>
    string PageSource { get; }

    /// <summary>
    /// Navigates the app to the background for the specified duration.
    /// </summary>
    void BackgroundApp(TimeSpan duration);

    /// <summary>
    /// Closes the application without terminating the driver session.
    /// </summary>
    public void CloseApp(string appId);

    /// <summary>
    /// Hides the keyboard if it is displayed.
    /// </summary>
    void HideKeyboard();

    /// <summary>
    /// Launches the application under test.
    /// </summary>
    public void LaunchApp(string appId);

    /// <summary>
    /// Terminates the driver session.
    /// </summary>
    void Quit();
}
