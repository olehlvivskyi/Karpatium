using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents a base class for UI layouts.
/// </summary>
/// <typeparam name="TElement"> The type of the element that serves as the layout wrapper. </typeparam>
public abstract class BaseLayout<TElement> where TElement : Element
{
    protected readonly TElement LayoutWrapper;
    protected readonly string PageName;
    protected abstract string LayoutName { get; }

    protected BaseLayout(TElement layoutWrapper, string pageName)
    {
        LayoutWrapper = layoutWrapper;
        PageName = pageName;
        
        WebManager.Waiter.ForElementIsVisible(LayoutWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}