using Karpatium.Core.Utilities;
using Karpatium.Core.Web;

namespace ToolsQa.UI;

/// <summary>
/// Represents the base class for all pages in the application.
/// </summary>
public abstract class BasePage
{
    private const int PageLoadTimeoutInSeconds = 30;
    
    internal BasePage(string relativePath)
    {
        ConditionalWaiter.ForTrue(() => WebManager.Browser.Url.Contains(relativePath), 
            $"Wait for `{relativePath}` page url failed.", PageLoadTimeoutInSeconds);
    }
}