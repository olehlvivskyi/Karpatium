using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Buttons" page.
/// </summary>
public sealed class ButtonsPage(string relativePath) : BasePage(relativePath)
{
    protected override string PageName => "\"Buttons\" page";
    
    private CommonElement ClickMe => ElementFactory.Create<CommonElement>(Selector.XPath("//button[text()='Click Me']"));
    private CommonElement ClickMeMessage => ElementFactory.Create<CommonElement>(Selector.Id("dynamicClickMessage"));
    private CommonElement DoubleClickMe => ElementFactory.Create<CommonElement>(Selector.Id("doubleClickBtn"));
    private CommonElement DoubleClickMeMessage => ElementFactory.Create<CommonElement>(Selector.Id("doubleClickMessage"));
    private CommonElement RightClickMe => ElementFactory.Create<CommonElement>(Selector.Id("rightClickBtn"));
    private CommonElement RightClickMeMessage => ElementFactory.Create<CommonElement>(Selector.Id("rightClickMessage"));

    /// <summary>
    /// Gets the text of the message associated with "Click Me" button.
    /// </summary>
    public string ClickMeMessageText
    {
        get
        {
            Log.Information("{PageName}: Checking the text of the message associated with \"Click Me\" button.", PageName);
            
            string clickMeMessageText = ClickMeMessage.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(ClickMeMessageText), clickMeMessageText);
            
            return clickMeMessageText;
        }
    }

    /// <summary>
    /// Gets the text of the message associated with "Double Click Me" button.
    /// </summary>
    public string DoubleClickMeMessageText
    {
        get
        {
            Log.Information("{PageName}: Checking the text of the message associated with \"Double Click Me\" button.", PageName);
            
            string doubleClickMeMessageText = DoubleClickMeMessage.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(DoubleClickMeMessageText), doubleClickMeMessageText);
            
            return doubleClickMeMessageText;
        }
    }

    /// <summary>
    /// Gets the text of the message associated with "Right Click Me" button.
    /// </summary>
    public string RightClickMeMessageText
    {
        get
        {
            Log.Information("{PageName}: Checking the text of the message associated with \"Right Click Me\" button.", PageName);
            
            string rightClickMeMessageText = RightClickMeMessage.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(RightClickMeMessageText), rightClickMeMessageText);
            
            return rightClickMeMessageText;
        }
    }

    /// <summary>
    /// Clicks on the "Click Me" button.
    /// </summary>
    public void ClickClickMe()
    {
        Log.Information("{PageName}: Clicking on the \"Click Me\" button.", PageName);
        
        ClickMe.Click();
    }

    /// <summary>
    /// Double-clicks on the "Double Click Me" button.
    /// </summary>
    public void ClickDoubleClickMe()
    {
        Log.Information("{PageName}: Double-clicking on the \"Double Click Me\" button.", PageName);
        
        DoubleClickMe.DoubleClick();
    }

    /// <summary>
    /// Right-clicks on the "Right Click Me" button.
    /// </summary>
    public void ClickRightClickMe()
    {
        Log.Information("{PageName}: Right-clicking on the \"Right Click Me\" button.", PageName);
        
        RightClickMe.RightClick();
    }
}