using System.Collections.Concurrent;
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

    /// <summary>
    /// Gets or sets the browser instance for the current test.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the browser is accessed before initialization.
    /// </exception>
    internal static Browser BrowserWrapper
    {
        get => Browsers[TestContext.CurrentContext.WorkerId ?? "single"]
               ?? throw new InvalidOperationException("WebManager is not initialized.");

        private set => Browsers[TestContext.CurrentContext.WorkerId ?? "single"] = value;
    }

    /// <summary>
    /// Provides access to the browser associated with the current test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the browser is accessed before initialization.
    /// </exception>
    public static IBrowser Browser => BrowserWrapper;

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