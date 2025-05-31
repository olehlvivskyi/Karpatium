using Karpatium.Core.Web.Elements;

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
    /// Waits until the specified element disappears within a given timeout period.
    /// </summary>
    /// <param name="element">The target element to monitor for disappearance.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to disappear. Default is 10 seconds.</param>
    public void ForElementDisappears(Element element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element exists within a given timeout period.
    /// </summary>
    /// <param name="element">The HTML element to check for existence.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to exist. Default is 10 seconds.</param>
    public void ForElementExists(Element element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element becomes invisible within a given timeout period.
    /// </summary>
    /// <param name="element">The element to monitor for invisibility.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to become invisible. Default is 10 seconds.</param>
    public void ForElementIsInvisible(Element element, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified element becomes visible within a given timeout period.
    /// </summary>
    /// <param name="element">The element for which visibility is being waited.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the element to become visible.
    /// Default is 10 seconds.</param>
    public void ForElementIsVisible(Element element, int timeOutInSeconds = 10);
    
    /// <summary>
    /// Waits until the specified JavaScript condition evaluates to false within a given timeout period.
    /// </summary>
    /// <param name="condition">The JavaScript condition, expressed as a string, to be evaluated repeatedly.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the condition to evaluate to false. Default is 10 seconds.</param>
    public void ForJavaScriptConditionToBeFalse(string condition, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified JavaScript condition evaluates to true within a given timeout period.
    /// </summary>
    /// <param name="condition">The JavaScript condition, expressed as a string, to be evaluated repeatedly.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the condition to evaluate to true. Default is 10 seconds.</param>
    public void ForJavaScriptConditionToBeTrue(string condition, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the page source remains unchanged for a specified timeout period.
    /// </summary>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the page source to remain unchanged.
    /// Default is 10 seconds.</param>
    /// <param name="pollingIntervalInMilliseconds">The interval, in milliseconds, at which the page source is checked.
    /// Default is 100 milliseconds.</param>
    public void ForPageSourceIsNotChanged(int timeOutInSeconds = 10, int pollingIntervalInMilliseconds = 100);

    /// <summary>
    /// Waits until the specified text is present within a given HTML element within a specified timeout period.
    /// </summary>
    /// <param name="element">The HTML element where the specified text is expected to be found.</param>
    /// <param name="text">The text to wait for within the specified element.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the text to appear in the element. Default is 10 seconds.</param>
    public void ForTextToBePresentInElement(CommonElement element, string text, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the specified text is present on the page's source within a given timeout period.
    /// </summary>
    /// <param name="text">The text to be searched for in the page's source.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the text to be present in the page's source. Default is 10 seconds.</param>
    public void ForTextToBePresentOnPage(string text, int timeOutInSeconds = 10);

    /// <summary>
    /// Waits until the current URL contains the specified URL fragment within a given timeout period.
    /// </summary>
    /// <param name="partOfUrl">The URL fragment to be matched within the browser's current URL.</param>
    /// <param name="timeOutInSeconds">The maximum duration, in seconds, to wait for the current URL to contain the specified URL fragment. Default is 10 seconds.</param>
    public void ForUrlToBe(string partOfUrl, int timeOutInSeconds = 10);
}