using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents the abstract base class for modals within the application.
/// </summary>
public abstract class BaseModal
{
    protected readonly Element ModalWrapper;
    protected readonly string PageName;

    protected BaseModal(Element modalWrapper, string pageName)
    {
        ModalWrapper = modalWrapper;
        PageName = pageName;
        
        WebManager.Waiter.ForElementIsVisible(ModalWrapper);
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}