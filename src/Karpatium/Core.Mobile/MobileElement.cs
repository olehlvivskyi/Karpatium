using System.Drawing;
using Karpatium.Core.Utilities;
using OpenQA.Selenium;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a base class for mobile elements.
/// </summary>
public abstract class MobileElement
{
    /// <summary>
    /// Represents the parent element of the current element if one exists.
    /// </summary>
    internal MobileElement? Parent { get; init; }

    /// <summary>
    /// Represents the selector used for locating UI elements on a mobile screen.
    /// </summary>
    internal MobileSelector? Selector { get; init; }

    /// <summary>
    /// Represents a wrapper for an element found as part of a multiple-element search.
    /// </summary>
    internal IWebElement? MultipleWrapper { get; init; }

    /// <summary>
    /// Gets the underlying IWebElement associated with this element.
    /// <br />The resolution logic works as follows:
    /// <br />- If the element was found as part of a search for multiple elements, <c>MultipleWrapper</c> is returned.
    /// <br />- If the element has a parent (<c>Parent</c>), it is located from the parent's context using the defined <c>Selector</c>.
    /// <br />- If no parent exists, the element is searched in the global view hierarchy using the <c>Selector</c>.
    /// <br />This property allows lazy resolution of the underlying element, ensuring it is fetched
    /// as-needed and always reflects the current state of the screen.
    /// </summary>
    internal IWebElement WebElementWrapper
    {
        get
        {
            IWebElement element = MultipleWrapper
                ?? (Parent == null
                    ? ConditionalWaiter.ForResult(() => MobileManager.MobileWrapper.FindElement(Selector!), $"{nameof(WebElementWrapper)} from global view hierarchy failed.")
                    : ConditionalWaiter.ForResult(() => Parent.FindElement(Selector!), $"{nameof(WebElementWrapper)} from parent element failed."));

            return element;
        }
    }

    private IWebElement FindElement(MobileSelector selector) =>
        WebElementWrapper.FindElement(selector.ByWrapper);

    internal IReadOnlyList<IWebElement> FindElements(MobileSelector selector) =>
        WebElementWrapper.FindElements(selector.ByWrapper);

    /// <summary>
    /// Indicates whether the element exists in the current view hierarchy.
    /// </summary>
    public bool Exists
    {
        get
        {
            try
            {
                IWebElement element = MultipleWrapper
                    ?? (Parent == null
                        ? MobileManager.MobileWrapper.FindElement(Selector!)
                        : Parent.FindElement(Selector!));

                return !string.IsNullOrEmpty(element.TagName);
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the element is enabled.
    /// </summary>
    public bool IsEnabled =>
        ConditionalWaiter.ForResult(() => WebElementWrapper.Enabled, $"{nameof(IsEnabled)} failed.");

    /// <summary>
    /// Indicates whether the element is currently visible on the screen.
    /// </summary>
    public bool IsVisible =>
        ConditionalWaiter.ForResult(() => Exists && WebElementWrapper.Displayed, $"{nameof(IsVisible)} failed.");

    /// <summary>
    /// Retrieves the tag name of the current element.
    /// </summary>
    public string TagName =>
        ConditionalWaiter.ForResult(() => WebElementWrapper.TagName, $"{nameof(TagName)} failed.");

    /// <summary>
    /// Simulates a tap (click) action on the element.
    /// </summary>
    public void Tap()
    {
        ConditionalWaiter.ForNoException(() => WebElementWrapper.Click(), $"{nameof(Tap)} failed.");
    }

    /// <summary>
    /// Gets the value of the specified attribute of the element.
    /// </summary>
    /// <param name="attribute">The name of the attribute to retrieve.</param>
    public string? GetAttribute(string attribute) =>
        ConditionalWaiter.ForResult(() => WebElementWrapper.GetAttribute(attribute), $"{nameof(GetAttribute)} failed.");

    /// <summary>
    /// Retrieves the bounding rectangle of the element, including its position and size.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> representing the location and dimensions of the element.</returns>
    public Rectangle GetBounds() =>
        ConditionalWaiter.ForResult(() => new Rectangle(WebElementWrapper.Location, WebElementWrapper.Size), $"{nameof(GetBounds)} failed.");
}
