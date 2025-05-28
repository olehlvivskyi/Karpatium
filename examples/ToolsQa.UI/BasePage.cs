using Karpatium.Core.Utilities;
using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents the base class for all pages in the Tools QA application.
/// </summary>
public abstract class BasePage
{
    internal BasePage(string relativePath)
    {
        ConditionalWaiter.ForTrue(() => WebManager.Browser.Url.Contains(relativePath), $"Wait for `{relativePath}` page url failed.");
        
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }
}