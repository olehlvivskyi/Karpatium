using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Dynamic Properties" page.
/// </summary>
public sealed class DynamicPropertiesPage(string relativePath) : BasePage(relativePath)
{
    private const string PageName = nameof(DynamicPropertiesPage);
    
    private CommonElement FirstButton => ElementFactory.Create<CommonElement>(Selector.Id("enableAfter"));
    private CommonElement SecondButton => ElementFactory.Create<CommonElement>(Selector.Id("colorChange"));
    private CommonElement ThirdButton => ElementFactory.Create<CommonElement>(Selector.Id("visibleAfter"));
    
    /// <summary>
    /// Gets a value indicating whether the "Will enable 5 seconds" button is enabled.
    /// </summary>
    public bool IsFirstButtonEnabled
    {
        get
        {
            Log.Information("{PageName}: Checking whether the \"Will enable 5 seconds\" button is enabled.", PageName);
            
            bool isFirstButtonEnabled = FirstButton.IsEnabled;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsFirstButtonEnabled), isFirstButtonEnabled);
            
            return isFirstButtonEnabled;
        }
    }
    
    /// <summary>
    /// Gets a value indicating whether the "Visible after 5 seconds" button is visible.
    /// </summary>
    public bool IsThirdButtonVisible
    {
        get
        {
            Log.Information("{PageName}: Checking whether the \"Visible after 5 seconds\" button is visible.", PageName);
            
            bool isThirdButtonVisible = ThirdButton.IsVisible;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsThirdButtonVisible), isThirdButtonVisible);
            
            return isThirdButtonVisible;
        }
    }
    
    /// <summary>
    /// Gets the text color of the "Color Change" button.
    /// </summary>
    public string SecondButtonTextColor
    {
        get
        {
            Log.Information("{PageName}: Checking the text color of the \"Color Change\" button.", PageName);
            
            string secondButtonTextColor = SecondButton.GetCssValue("color");
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(SecondButtonTextColor), secondButtonTextColor);
            
            return secondButtonTextColor;
        }
    }
}