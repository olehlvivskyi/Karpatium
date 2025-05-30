using Karpatium.Core.Web;

namespace ToolsQa.UI;

public abstract class BaseComponent
{
    internal readonly Element ComponentWrapper;

    protected BaseComponent(Element componentWrapper)
    {
        ComponentWrapper = componentWrapper;
        
        WebManager.Waiter.ForElementIsVisible(ComponentWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}