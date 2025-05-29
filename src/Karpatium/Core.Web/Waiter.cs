using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a waiter instance for managing test execution.
/// </summary>
internal sealed class Waiter(Browser browser) : IWaiter
{
    private Browser BrowserWrapper { get; } = browser;
    
    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100)
    {
        ConditionalWaiter.ForTrue(() =>
        {
            var previousPageSource = BrowserWrapper.PageSource;
            Thread.Sleep(pollingIntervalInMilliseconds);
            var currentPageSource = BrowserWrapper.PageSource;
            return currentPageSource == previousPageSource;
        }, $"{nameof(Waiter)}: {nameof(ForPageSourceIsNotChanged)} failed.", timeOutInSeconds);
    }
}