using System.Collections.Concurrent;
using Karpatium.Core.Utilities;
using NUnit.Framework;
using Serilog;

namespace Karpatium.Core.Web;

/// <summary>
/// Manages browser instances for tests.
/// </summary>
/// <remarks>
/// Ensures thread-safety and proper handling for parallel execution.
/// </remarks>
public static class WebManager
{
    private static ConcurrentDictionary<string, Browser> Browsers { get; } = new();
    private static ConcurrentDictionary<string, Waiter> Waiters { get; } = new();

    /// <summary>
    /// Gets or sets the browser instance for the current test.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the browser is accessed before initialization.
    /// </exception>
    internal static Browser BrowserWrapper
    {
        get => Browsers[TestContext.CurrentContext.WorkerId ?? "single"]
               ?? throw new InvalidOperationException($"{nameof(WebManager)} is not initialized.");

        private set => Browsers[TestContext.CurrentContext.WorkerId ?? "single"] = value;
    }

    /// <summary>
    /// Gets or sets the waiter instance for managing wait conditions.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the waiter is accessed before initialization.
    /// </exception>
    private static Waiter WaiterWrapper
    {
        get => Waiters[TestContext.CurrentContext.WorkerId ?? "single"]
               ?? throw new InvalidOperationException($"{nameof(WebManager)} is not initialized.");

        set => Waiters[TestContext.CurrentContext.WorkerId ?? "single"] = value;
    }

    /// <summary>
    /// Provides access to the browser associated with the current test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the browser is accessed before initialization.
    /// </exception>
    public static IBrowser Browser => BrowserWrapper;

    /// <summary>
    /// Provides access to the waiter associated with the current test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if WebWaiter is accessed before initialization.
    /// </exception>
    public static IWaiter Waiter => WaiterWrapper;

    /// <summary>
    /// Initializes a new browser instance using the provided settings.
    /// </summary>
    /// <param name="browserSettings">Settings for configuring the browser instance.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the provided <paramref name="browserSettings"/> is null.
    /// </exception>
    public static void Initialize(IBrowserSettings browserSettings)
    {
        if (browserSettings == null)
        {
            throw new ArgumentNullException(nameof(browserSettings), "Browser settings cannot be null.");
        }
        
        Log.Verbose("WebManager: Initializing `{BrowserType}` browser.", browserSettings.BrowserType);
        Log.Verbose("WebManager: IBrowserSettings = {@BrowserSettings}", browserSettings);
        
        IBrowserFactory browserFactory = BrowserFactory.Get(browserSettings);
        BrowserWrapper = browserFactory.CreateBrowser(browserSettings);
        WaiterWrapper = new Waiter(BrowserWrapper);

        string downloadedFilesFolder = PathUtils.GetLocalUserPath(browserSettings.DownloadedFilesFolderName);
        Log.Verbose("WebManager: Creating `{DownloadedFilesFolder} folder for downloaded files.", downloadedFilesFolder);
        Directory.CreateDirectory(downloadedFilesFolder);
    }

    /// <summary>
    /// Closes the browser associated with the current test.
    /// </summary>
    public static void Quit()
    {
        Log.Verbose("WebManager: Quitting browser.");
        
        BrowserWrapper.Quit();
    }
}