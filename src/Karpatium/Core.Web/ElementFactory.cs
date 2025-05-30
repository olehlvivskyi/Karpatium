namespace Karpatium.Core.Web;

/// <summary>
/// Provides a factory for creating instances of elements or collections of elements within a web application.
/// </summary>
public static class ElementFactory
{
    /// <summary>
    /// Creates an instance of a specific element type with a given selector and optional parent element.
    /// </summary>
    /// <typeparam name="TElement">The type of the element to create, which inherit from <see cref="Element"/> .</typeparam>
    /// <param name="selector">The selector that identifies the element within the web application.</param>
    /// <param name="parent">The optional parent element under which this element is scoped.</param>
    public static TElement Create<TElement>(Selector selector, Element? parent = null)
        where TElement : Element, new()
    {
        return new TElement
        {
            Selector = selector,
            Parent = parent
        };
    }

    /// <summary>
    /// Creates a collection of a specific element type with a given selector and optional parent element.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements in the collection, which inherit from <see cref="Element"/>.</typeparam>
    /// <param name="selector">The selector that identifies the elements within the web application.</param>
    /// <param name="parent">The optional parent element under which this collection is scoped.</param>
    public static ElementCollection<TElement> CreateMultiple<TElement>(Selector selector, Element? parent = null)
        where TElement : Element, new()
    {
        return new ElementCollection<TElement>
        {
            Selector = selector,
            Parent = parent
        };
    }
}