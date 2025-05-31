using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents a base class for UI components.
/// </summary>
/// <typeparam name="TElement"> The type of the element that serves as the component wrapper. </typeparam>
/// <remarks>
/// The <see cref="BaseComponent{TElement}"/> class is designed to encapsulate the common functionality shared by various UI components.
/// </remarks>
public abstract class BaseComponent<TElement> where TElement : Element
{
    internal readonly TElement ComponentWrapper;

    protected BaseComponent(TElement componentWrapper)
    {
        ComponentWrapper = componentWrapper;
        
        WebManager.Waiter.ForElementIsVisible(ComponentWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}