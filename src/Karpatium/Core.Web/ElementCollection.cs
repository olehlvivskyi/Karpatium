using System.Collections;
using Karpatium.Core.Utilities;
using OpenQA.Selenium;

namespace Karpatium.Core.Web;

/// <summary>
/// Represents a base class for a collection of HTML elements.
/// </summary>
/// <typeparam name="TElement">
/// The type of elements in the collection. Must inherit from <see cref="Element"/> and
/// have a parameterless constructor.
/// </typeparam>
/// <remarks>
/// This class allows accessing elements matching a specific selector, optionally scoped
/// to a parent element. It leverages lazy evaluation to find elements when accessed.
/// </remarks>
public class ElementCollection<TElement> : IReadOnlyList<TElement>
    where TElement : Element, new()
{
    /// <summary>
    /// Represents the parent element of the current collection of elements, if one exists.
    /// </summary>
    internal Element? Parent { get; init; }
    
    /// <summary>
    /// Represents the selector used for locating UI elements on a web page.
    /// </summary>
    internal Selector? Selector { get; init; }
    
    private IReadOnlyList<TElement> Elements => WebElementsWrapper
        .Select(el => new TElement { MultipleWrapper = el })
        .ToList();
    
    /// <summary>
    /// Gets the underlying collection of IWebElement elements.
    /// <br />The resolution logic works as follows:
    /// <br />- If the element has a parent (`Parent`), it is located from the parent's context
    /// using the defined `Selector`.
    /// <br />- If no parent exists, the element is searched in the global DOM using the `Selector`.
    /// <br />This property allows lazy resolution of the underlying collection of web elements, ensuring it is fetched
    /// as-needed and always reflects the current state of the page.
    /// </summary>
    /// <returns>
    /// A read only collection of <see cref="IWebElement"/> instances representing the elements.
    /// </returns>
    private IReadOnlyList<IWebElement> WebElementsWrapper
    {
        get
        {
            IReadOnlyList<IWebElement> elements = Parent == null 
                ? ConditionalWaiter.ForResult(() => WebManager.BrowserWrapper.FindElements(Selector!),
                    $"{nameof(WebElementsWrapper)} from global DOM failed.") 
                : ConditionalWaiter.ForResult(() => Parent.FindElements(Selector!),
                    $"{nameof(WebElementsWrapper)} from parent element failed.");
            
            return elements;
        }
    }

    /// <summary>
    /// Gets HTML element at the specified index in the collection.
    /// </summary>
    /// <param name="index">The index of HTML element to retrieve.</param>
    /// <returns>HTML element at the specified index.</returns>
    public TElement this[int index] => Elements[index];

    /// <summary>
    /// Gets the number of HTML elements in the collection.
    /// </summary>
    public int Count => Elements.Count;

    /// <summary>
    /// Returns an enumerator that iterates through HTML elements.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through HTML elements.
    /// </summary>
    /// <returns>An enumerator for the collection.</returns>
    public IEnumerator<TElement> GetEnumerator() => Elements.GetEnumerator();
}