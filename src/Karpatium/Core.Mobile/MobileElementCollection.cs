using System.Collections;
using Karpatium.Core.Utilities;
using OpenQA.Selenium;

namespace Karpatium.Core.Mobile;

/// <summary>
/// Represents a base class for a collection of mobile elements.
/// </summary>
/// <typeparam name="TElement">
/// The type of elements in the collection. Must inherit from <see cref="MobileElement"/> and
/// have a parameterless constructor.
/// </typeparam>
/// <remarks>
/// This class allows accessing elements matching a specific selector, optionally scoped
/// to a parent element. It leverages lazy evaluation to find elements when accessed.
/// </remarks>
public class MobileElementCollection<TElement> : IReadOnlyList<TElement>
    where TElement : MobileElement, new()
{
    /// <summary>
    /// Represents the parent element of the current collection, if one exists.
    /// </summary>
    internal MobileElement? Parent { get; init; }

    /// <summary>
    /// Represents the selector used for locating UI elements on a mobile screen.
    /// </summary>
    internal MobileSelector? Selector { get; init; }

    private IReadOnlyList<TElement> Elements => WebElementsWrapper
        .Select(el => new TElement { MultipleWrapper = el })
        .ToList();

    /// <summary>
    /// Gets the underlying collection of IWebElement elements.
    /// <br />The resolution logic works as follows:
    /// <br />- If the collection has a parent (<c>Parent</c>), elements are located from the parent's context using the defined <c>Selector</c>.
    /// <br />- If no parent exists, elements are searched in the global view hierarchy using the <c>Selector</c>.
    /// <br />This property allows lazy resolution of the underlying collection, ensuring it is fetched
    /// as-needed and always reflects the current state of the screen.
    /// </summary>
    private IReadOnlyList<IWebElement> WebElementsWrapper
    {
        get
        {
            return Parent == null
                ? ConditionalWaiter.ForResult(
                    () => MobileManager.MobileWrapper.FindElements(Selector!),
                    $"{nameof(WebElementsWrapper)} from global view hierarchy failed.")
                : ConditionalWaiter.ForResult(
                    () => Parent.FindElements(Selector!),
                    $"{nameof(WebElementsWrapper)} from parent element failed.");
        }
    }

    /// <summary>
    /// Gets the mobile element at the specified index in the collection.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    public TElement this[int index] => Elements[index];

    /// <summary>
    /// Gets the number of mobile elements in the collection.
    /// </summary>
    public int Count => Elements.Count;

    /// <summary>
    /// Returns an enumerator that iterates through the mobile elements.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through the mobile elements.
    /// </summary>
    public IEnumerator<TElement> GetEnumerator() => Elements.GetEnumerator();
}
