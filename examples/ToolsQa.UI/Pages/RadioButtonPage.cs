using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Radio Button" page.
/// </summary>
public sealed class RadioButtonPage(string relativePath) : BasePage(relativePath)
{
    protected override string PageName => "\"Radio Button\" page";

    private RadioButtonElement Impressive => ElementFactory.Create<RadioButtonElement>(Selector.Id("impressiveRadio"));
    private CommonElement ImpressiveLabel => ElementFactory.Create<CommonElement>(Selector.Css("label[for='impressiveRadio']"));
    private RadioButtonElement No => ElementFactory.Create<RadioButtonElement>(Selector.Id("noRadio"));
    private CommonElement TextSuccess => ElementFactory.Create<CommonElement>(Selector.Class("text-success"));
    private RadioButtonElement Yes => ElementFactory.Create<RadioButtonElement>(Selector.Id("yesRadio"));
    private CommonElement YesLabel => ElementFactory.Create<CommonElement>(Selector.Css("label[for='yesRadio']"));

    /// <summary>
    /// Gets a value indicating whether the "Impressive" radio button is checked.
    /// </summary>
    public bool IsImpressiveChecked
    {
        get
        {
            Log.Information("{PageName}: Checking whether the \"Impressive\" radio button is checked.", PageName);
            
            bool isImpressiveChecked = Impressive.IsChecked;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsImpressiveChecked), isImpressiveChecked);
            
            return isImpressiveChecked;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the "No" radio button is enabled.
    /// </summary>
    public bool IsNoEnabled
    {
        get
        {
            Log.Information("{PageName}: Checking whether the \"No\" radio button is enabled.", PageName);
            
            bool isNoEnabled = No.IsEnabled;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsNoEnabled), isNoEnabled);
            
            return isNoEnabled;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the "Yes" radio button is checked.
    /// </summary>
    public bool IsYesChecked
    {
        get
        {
            Log.Information("{PageName}: Checking whether the \"Yes\" radio button is checked.", PageName);

            bool isYesChecked = Yes.IsChecked;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(IsYesChecked), isYesChecked);
            
            return isYesChecked;
        }
    }

    /// <summary>
    /// Gets the text displayed for the currently selected radio button.
    /// </summary>
    public string SelectedRadioButtonText
    {
        get
        {
            Log.Information("{PageName}: Checking the text displayed for the currently selected radio button.", PageName);
            
            string selectedRadioButtonText = TextSuccess.Text;
            Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, selectedRadioButtonText);
            
            return selectedRadioButtonText;
        }
    }

    /// <summary>
    /// Checks the "Impressive" radio button.
    /// </summary>
    public void CheckImpressive()
    {
        Log.Information("{PageName}: Checking the \"Impressive\" radio button.", PageName);

        if (!Impressive.IsChecked)
        {
            ImpressiveLabel.Click();
        }
    }
    
    /// <summary>
    /// Checks the "Yes" radio button.
    /// </summary>
    public void CheckYes()
    {
        Log.Information("{PageName}: Checking the \"Yes\" radio button.", PageName);
        
        if (!Yes.IsChecked)
        {
            YesLabel.Click();
        }
    }
}