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
        ConditionalWaiter.ForTrue(() => WebManager.Browser.Url.Contains(relativePath), $"{GetPageName()}: Wait for `{relativePath}` page url failed.");
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
    
    private string GetPageName() => PageName;
}