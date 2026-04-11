using Karpatium.Core.Mobile.MobileElements;
using Karpatium.Core.Utilities;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a waiter instance for managing test execution on mobile.
/// </summary>
internal sealed class MobileWaiter(Mobile mobile) : IMobileWaiter
{
    private IMobileWaiter _mobileWaiterImplementation;
    private Mobile MobileWrapper { get; } = mobile;

    public void ForElementDisappears(MobileElement element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForFalse(() => element.Exists, $"{nameof(MobileWaiter)}: {nameof(ForElementDisappears)} failed.", timeOutInSeconds);
    }

    public void ForElementExists(MobileElement element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.Exists, $"{nameof(MobileWaiter)}: {nameof(ForElementExists)} failed.", timeOutInSeconds);
    }

    public void ForElementIsInvisible(MobileElement element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForFalse(() => element.IsVisible, $"{nameof(MobileWaiter)}: {nameof(ForElementIsInvisible)} failed.", timeOutInSeconds);
    }

    public void ForElementIsVisible(MobileElement element, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.IsVisible, $"{nameof(MobileWaiter)}: {nameof(ForElementIsVisible)} failed.", timeOutInSeconds);
    }

    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100)
    {
        ConditionalWaiter.ForTrue(() =>
        {
            var previousPageSource = MobileWrapper.PageSource;
            Thread.Sleep(pollingIntervalInMilliseconds);
            var currentPageSource = MobileWrapper.PageSource;
            return currentPageSource == previousPageSource;
        }, $"{nameof(MobileWaiter)}: {nameof(ForPageSourceIsNotChanged)} failed.", timeOutInSeconds);
    }

    public void ForTextToBePresentInElement(MobileCommonElement element, string text, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => element.Text.Contains(text), $"{nameof(MobileWaiter)}: {nameof(ForTextToBePresentInElement)} `{text}` failed.", timeOutInSeconds);
    }

    public void ForTextToBePresentOnPage(string text, int timeOutInSeconds = 10)
    {
        ConditionalWaiter.ForTrue(() => MobileWrapper.PageSource.Contains(text), $"{nameof(MobileWaiter)}: {nameof(ForTextToBePresentOnPage)} `{text}` failed.", timeOutInSeconds);
    }
}
