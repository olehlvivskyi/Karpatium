using Karpatium.Core.Utilities;
using Karpatium.Core.Web.Elements;

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
        }, $"{nameof(Waiter)}: {nameof(ForDocumentReadyStateToBeComplete)} failed.", timeOutInSeconds);
    }

    public void ForElementDisappears(Element element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForFalse(() => element.Exists, $"{nameof(Waiter)}: {nameof(ForElementDisappears)} failed.", timeOutInSeconds);
    }

    public void ForElementExists(Element element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.Exists, $"{nameof(Waiter)}: {nameof(ForElementExists)} failed.", timeOutInSeconds);
    }
    
    public void ForElementIsInvisible(Element element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForFalse(() => element.IsVisible, $"{nameof(Waiter)}: {nameof(ForElementIsInvisible)} failed.", timeOutInSeconds);
    }
    
    public void ForElementIsVisible(Element element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.IsVisible, $"{nameof(Waiter)}: {nameof(ForElementIsVisible)} failed.", timeOutInSeconds);
    }

    public void ForJavaScriptConditionToBeFalse(string condition, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => BrowserWrapper.ExecuteJavascript(condition)?.ToString() == "false", $"{nameof(Waiter)}: {nameof(ForJavaScriptConditionToBeFalse)} failed.", timeOutInSeconds);
    }

    public void ForJavaScriptConditionToBeTrue(string condition, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => BrowserWrapper.ExecuteJavascript(condition)?.ToString() == "true", $"{nameof(Waiter)}: {nameof(ForJavaScriptConditionToBeTrue)} failed.", timeOutInSeconds);
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

    public void ForTextToBePresentInElement(CommonElement element, string text, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.Text.Contains(text), $"{nameof(Waiter)}: {nameof(ForTextToBePresentInElement)} `{text}` failed.", timeOutInSeconds);
    }

    public void ForTextToBePresentOnPage(string text, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => BrowserWrapper.PageSource.Contains(text), $"{nameof(Waiter)}: {nameof(ForTextToBePresentOnPage)} `{text}` failed.", timeOutInSeconds);
    }
    
    public void ForUrlToBe(string partOfUrl, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => BrowserWrapper.Url.Contains(partOfUrl), $"{nameof(Waiter)}: {nameof(ForUrlToBe)} `{partOfUrl}` failed.", timeOutInSeconds);
    }
}