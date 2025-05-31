using System.Collections.Concurrent;
using System.Reflection;
using Karpatium.Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
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
    /// Gets the type of the browser that is currently being used for test execution.
    /// </summary>
    public static BrowserType CurrentBrowserType { get; private set; }
    
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
    
    internal static int DemoModeDelayInMilliseconds { get; private set; }
    
    /// <summary>
    /// Demo mode introduces delays and highlights elements, typically used to provide additional visual feedback. 
    /// </summary>
    internal static bool IsDemoModeEnabled { get; private set; }

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
        Log.Verbose("WebManager: IBrowserSettings = `{@BrowserSettings}`", browserSettings);
        
        IBrowserFactory browserFactory = BrowserFactory.Get(browserSettings);
        BrowserWrapper = browserFactory.CreateBrowser(browserSettings);
        WaiterWrapper = new Waiter(BrowserWrapper);
        CurrentBrowserType = browserSettings.BrowserType;
        IsDemoModeEnabled = browserSettings.IsDemoModeEnabled;
        if (IsDemoModeEnabled)
        {
            DemoModeDelayInMilliseconds = browserSettings.DemoModeDelayInMilliseconds;
        }

        string downloadedFilesFolder = PathUtils.GetLocalUserPath(browserSettings.DownloadedFilesFolderName);
        Log.Verbose("WebManager: Ensuring that `{DownloadedFilesFolder} folder for downloaded files is created.", downloadedFilesFolder);
        Directory.CreateDirectory(downloadedFilesFolder);
    }

    /// <summary>
    /// Generates browser log files for debugging purposes and saves it to a specified location.
    /// </summary>
    /// <param name="fileName">The name of the file (excluding extension) where the browser logs should be saved.</param>
    public static string DebugBrowserLogs(string fileName)
    {
        FileInfo fileInfo = new FileInfo(Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs", $"{fileName}.browserlogs.txt"));
        Directory.CreateDirectory(fileInfo.DirectoryName!);
        File.WriteAllText(fileInfo.FullName, BrowserWrapper.GetBrowserLogs().Select(log => $"[{log.Timestamp} {log.Level}] {log.Message}").Aggregate((previous, next) => $"{previous}\n{next}"));
        
        return fileInfo.FullName;
    }

    /// <summary>
    /// Saves the current page source of the browser session to a text file with the specified file name for debugging purposes.
    /// </summary>
    /// <param name="fileName">The name of the file (without extension) to store the page source.</param>
    /// <returns>The full path to the file containing the page source.</returns>
    public static string DebugPageSource(string fileName)
    {
        FileInfo fileInfo = new FileInfo(Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs", $"{fileName}.pageSource.txt"));
        Directory.CreateDirectory(fileInfo.DirectoryName!);
        File.WriteAllText(fileInfo.FullName, Browser.PageSource);
        
        return fileInfo.FullName;
    }

    /// <summary>
    /// Captures a screenshot of the current browser window and saves it to the specified file path.
    /// </summary>
    /// <param name="fileName">The name of the file (without extension) where the screenshot will be saved.</param>
    /// <returns>The full path of the file where the screenshot was saved.</returns>
    public static string DebugScreenshot(string fileName)
    {
        FileInfo fileInfo = new FileInfo(Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs", $"{fileName}.screenshot.png"));
        Directory.CreateDirectory(fileInfo.DirectoryName!);
        Screenshot screenshot = BrowserWrapper.GetScreenshot();
        screenshot.SaveAsFile(fileInfo.FullName);
        
        return fileInfo.FullName;
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