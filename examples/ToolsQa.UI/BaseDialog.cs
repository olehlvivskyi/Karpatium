using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents a base class for UI dialogs.
/// </summary>
/// <typeparam name="TElement"> The type of the element that serves as the dialog wrapper. </typeparam>
public abstract class BaseDialog<TElement> where TElement : Element
{
    protected readonly TElement DialogWrapper;
    protected readonly string PageName;
    protected abstract string DialogName { get; }

    protected BaseDialog(TElement dialogWrapper, string pageName)
    {
        DialogWrapper = dialogWrapper;
        PageName = pageName;
        
        WebManager.Waiter.ForElementIsVisible(DialogWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}