using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Broken" page.
/// </summary>
public sealed class BrokenPage(string relativePath) : BasePage(relativePath)
{
    protected override string PageName => "\"Broken\" page";
    
    private ImageElement FirstLogo => ElementFactory.Create<ImageElement>(Selector.XPath("//p[text()='Valid image']/following-sibling::img"));
    private ImageElement SecondLogo => ElementFactory.Create<ImageElement>(Selector.XPath("//p[text()='Broken image']/following-sibling::img"));

    /// <summary>
    /// Gets a value indicating whether the first logo image is valid.
    /// </summary>
    public bool IsFirstLogoImageValid
    {
        get
        {
            Log.Information("{PageName}: Checking whether the first logo image is valid.", PageName);

            bool isFirstLogoImageValid = FirstLogo.IsImageValid;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsFirstLogoImageValid), isFirstLogoImageValid);
            
            return isFirstLogoImageValid;
        }
    }
    
    /// <summary>
    /// Gets a value indicating whether the second logo image is valid.
    /// </summary>
    public bool IsSecondLogoImageValid
    {
        get
        {
            Log.Information("{PageName}: Checking whether the second logo image is valid.", PageName);
            
            bool isSecondLogoImageValid = SecondLogo.IsImageValid;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsSecondLogoImageValid), isSecondLogoImageValid);

            return isSecondLogoImageValid;
        }
    }
}