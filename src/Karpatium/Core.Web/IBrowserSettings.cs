namespace Karpatium.Core.Web;

/// <summary>
/// Represents configuration settings for browser instances.
/// </summary>
public interface IBrowserSettings
{
    /// <summary>
    /// Gets the type of the browser to be used in tests.
    /// </summary>
    /// <remarks>
    /// The <see cref="BrowserType"/> specifies the browser (e.g., Chrome) that will be instantiated by the framework.
    /// </remarks>
    BrowserType BrowserType { get; }

    /// <summary>
    /// Gets the downloaded files folder name.
    /// </summary>
    /// <remarks>
    /// The <see cref="DownloadedFilesFolderName"/> property defines the folder name where files downloaded by the browser will be stored.
    /// This folder is typically named "Downloads".
    /// </remarks>
    string DownloadedFilesFolderName { get; }

    /// <summary>
    /// Indicates whether the browser should run in headless mode.
    /// </summary>
    /// <remarks>
    /// Headless mode runs the browser without a graphical user interface (GUI).
    /// </remarks>
    bool IsHeadlessEnabled { get; }
    
    /// <summary>
    /// Indicates whether tests are being executed locally or in a remote environment.
    /// </summary>
    /// <remarks>
    /// Use this property to determine whether the tests should run on a local 
    /// machine or in a distributed/remote execution environment.
    /// </remarks>
    bool IsLocalExecution { get; }
    
    /// <summary>
    /// Gets the URL of the remote WebDriver server.
    /// </summary>
    /// <remarks>
    /// This property is used for configuring remote execution in cases where tests 
    /// are executed using Selenium Grid, cloud-based services (e.g., BrowserStack, Sauce Labs), 
    /// or other remote WebDriver instances.
    /// If <see cref="IsLocalExecution"/> is set to <c>true</c>, this property can be empty string.
    /// </remarks>
    string RemoteUrl { get; }
}