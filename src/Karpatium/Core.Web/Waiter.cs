using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a waiter instance for managing test execution.
/// </summary>
internal sealed class Waiter(Browser browser) : IWaiter
{
    private Browser BrowserWrapper { get; } = browser;

    /// <summary>
    /// Waits until the page source remains unchanged for a specified timeout period.
    /// </summary>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the page source to remain unchanged.
    /// Default is 10 seconds.</param>
    /// <param name="pollingIntervalInMilliseconds">The interval, in milliseconds, at which the page source is checked.
    /// Default is 100 milliseconds.</param>
    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100)
    {
        ConditionalWaiter.ForTrue(() =>
        {
            var previousPageSource = BrowserWrapper.PageSource;
            Thread.Sleep(pollingIntervalInMilliseconds);
            var currentPageSource = BrowserWrapper.PageSource;
            return currentPageSource == previousPageSource;
        }, $"`{nameof(ForPageSourceIsNotChanged)}` failed.", timeOutInSeconds);
    }
}