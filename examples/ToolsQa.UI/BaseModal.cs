using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents a base class for UI modals.
/// </summary>
/// <typeparam name="TElement"> The type of the element that serves as the modal wrapper. </typeparam>
public abstract class BaseModal<TElement> where TElement : Element
{
    protected readonly TElement ModalWrapper;
    protected readonly string PageName;

    protected BaseModal(TElement modalWrapper, string pageName)
    {
        ModalWrapper = modalWrapper;
        PageName = pageName;
        
        WebManager.Waiter.ForElementIsVisible(ModalWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}