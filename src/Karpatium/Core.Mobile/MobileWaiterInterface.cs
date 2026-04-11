using Karpatium.Core.Mobile.MobileElements;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a waiter instance for managing test execution on mobile.
/// </summary>
public interface IMobileWaiter
{
    /// <summary>
    /// Waits until the specified element disappears within a given timeout period.
    /// </summary>
    /// <param name="element">The target element to monitor for disappearance.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to disappear. Default is 10 seconds.</param>
    public void ForElementDisappears(MobileElement element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element exists within a given timeout period.
    /// </summary>
    /// <param name="element">The element to check for existence.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to exist. Default is 10 seconds.</param>
    public void ForElementExists(MobileElement element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element becomes invisible within a given timeout period.
    /// </summary>
    /// <param name="element">The element to monitor for invisibility.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to become invisible. Default is 10 seconds.</param>
    public void ForElementIsInvisible(MobileElement element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element becomes visible within a given timeout period.
    /// </summary>
    /// <param name="element">The element for which visibility is being waited.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to become visible. Default is 10 seconds.</param>
    public void ForElementIsVisible(MobileElement element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the page source remains unchanged within a given timeout period.
    /// </summary>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the page source to remain unchanged. Default is 10 seconds.</param>
    /// <param name="pollingIntervalInMilliseconds">The interval, in milliseconds, at which the page source is checked. Default is 100 milliseconds.</param>
    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100);

    /// <summary>
    /// Waits until the specified text is present within a given element within a specified timeout period.
    /// </summary>
    /// <param name="element">The element where the specified text is expected to be found.</param>
    /// <param name="text">The text to wait for within the specified element.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the text to appear. Default is 10 seconds.</param>
    public void ForTextToBePresentInElement(MobileCommonElement element, string text, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified text is present in the page source within a given timeout period.
    /// </summary>
    /// <param name="text">The text to search for in the page source.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the text to be present. Default is 10 seconds.</param>
    public void ForTextToBePresentOnPage(string text, int timeOutInSeconds = 10);
}
