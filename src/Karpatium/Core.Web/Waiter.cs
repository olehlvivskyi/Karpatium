using Karpatium.Core.Utilities;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a waiter instance for managing test execution.
/// </summary>
internal sealed class Waiter(Browser browser) : IWaiter
{
    private Browser BrowserWrapper { get; } = browser;

    public void ForDocumentReadyStateToBeComplete(int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() =>
        {
            string documentReadyState = BrowserWrapper.ExecuteJavascript("return document.readyState")?.ToString() ?? string.Empty;
            return documentReadyState.Equals("complete");
        }, "Wait for document ready state to be complete.", timeOutInSeconds);
    }
    
    public void ForElementIsVisible(Element element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.IsVisible, $"{nameof(Waiter)}: {nameof(ForElementIsVisible)} failed.", timeOutInSeconds);
    }

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