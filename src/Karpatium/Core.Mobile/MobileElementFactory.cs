namespace Karpatium.Core.Mobile;

/// <summary>
/// Provides a factory for creating instances of mobile elements or collections of elements.
/// </summary>
public static class MobileElementFactory
{
    /// <summary>
    /// Creates an instance of a specific mobile element type with a given selector and optional parent element.
    /// </summary>
    /// <typeparam name="TElement">The type of the element to create, which must inherit from <see cref="MobileElement"/>.</typeparam>
    /// <param name="selector">The selector that identifies the element within the mobile screen.</param>
    /// <param name="parent">The optional parent element under which this element is scoped.</param>
    public static TElement Create<TElement>(MobileSelector selector, MobileElement? parent = null)
        where TElement : MobileElement, new()
    {
        return new TElement
        {
            Selector = selector,
            Parent = parent
        };
    }

    /// <summary>
    /// Creates a collection of a specific mobile element type with a given selector and optional parent element.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements in the collection, which must inherit from <see cref="MobileElement"/>.</typeparam>
    /// <param name="selector">The selector that identifies the elements within the mobile screen.</param>
    /// <param name="parent">The optional parent element under which this collection is scoped.</param>
    public static MobileElementCollection<TElement> CreateMultiple<TElement>(MobileSelector selector, MobileElement? parent = null)
        where TElement : MobileElement, new()
    {
        return new MobileElementCollection<TElement>
        {
            Selector = selector,
            Parent = parent
        };
    }
}
