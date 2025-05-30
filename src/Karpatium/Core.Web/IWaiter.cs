namespace Karpatium.Core.Web;

/// <summary>
/// Represents a waiter instance for managing test execution.
/// </summary>
public interface IWaiter
{
    /// <summary>
    /// Waits until the document's ready state becomes "complete" within a given timeout period.
    /// </summary>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the document ready state to change to "complete".
    /// Default is 10 seconds.</param>
    public void ForDocumentReadyStateToBeComplete(int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element becomes visible within a given timeout period.
    /// </summary>
    /// <param name="element">The element for which visibility is being waited.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to become visible.
    /// Default is 10 seconds.</param>
    public void ForElementIsVisible(Element element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the page source remains unchanged for a specified timeout period.
    /// </summary>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the page source to remain unchanged.
    /// Default is 10 seconds.</param>
    /// <param name="pollingIntervalInMilliseconds">The interval, in milliseconds, at which the page source is checked.
    /// Default is 100 milliseconds.</param>
    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100);
}