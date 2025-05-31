using Karpatium.Core.Utilities;
using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents the base class for all pages in the Tools QA application.
/// </summary>
public abstract class BasePage
{
    protected abstract string PageName { get; }
    
    internal BasePage(string relativePath)
    {
        WebManager.Waiter.ForUrlToBe(relativePath);
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
    
    private string GetPageName() => PageName;
}