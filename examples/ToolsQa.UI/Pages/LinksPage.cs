using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Links" page.
/// </summary>
public sealed class LinksPage(string relativePath) : BasePage(relativePath)
{
    private const string PageName = nameof(LinksPage);
    
    private CommonElement Created => ElementFactory.Create<CommonElement>(Selector.Id("created"));
    private CommonElement Home => ElementFactory.Create<CommonElement>(Selector.Id("simpleLink"));
    private CommonElement NotFound => ElementFactory.Create<CommonElement>(Selector.Id("invalid-url"));
    private CommonElement LinkResponse => ElementFactory.Create<CommonElement>(Selector.Id("linkResponse"));

    /// <summary>
    /// Gets the text of the link response message.
    /// </summary>
    public string LinkResponseText
    {
        get
        {
            Log.Information("{PageName}: Checking the text of the link response message.", PageName);
            
            string linkResponseText = LinkResponse.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(LinkResponseText), linkResponseText);
            
            return linkResponseText;
        }
    }
    
    /// <summary>
    /// Clicks on the "Created" link.
    /// </summary>
    public void ClickCreated()
    {
        Log.Information("{PageName}: Clicking on the \"Created\" link.", PageName);
        
        Created.Click();
    }

    /// <summary>
    /// Clicks on the "Home" link.
    /// </summary>
    public void ClickHome()
    {
        Log.Information("{PageName}: Clicking on the \"Home\" link.", PageName);
        
        Home.Click();
    }
    
    /// <summary>
    /// Clicks on the "Not Found" link.
    /// </summary>
    public void ClickNotFound()
    {
        Log.Information("{PageName}: Clicking \"Not Found\" link.", PageName);
        
        NotFound.Click();
    }
}