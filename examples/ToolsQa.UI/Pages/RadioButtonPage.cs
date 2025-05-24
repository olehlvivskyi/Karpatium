using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Represents the "RadioButton" page.
/// </summary>
public sealed class RadioButtonPage(string relativePath) : BasePage(relativePath)
{
    private RadioButtonElement Impressive => ElementFactory.Create<RadioButtonElement>(Selector.Id("impressiveRadio"));
    private CommonElement ImpressiveLabel => ElementFactory.Create<CommonElement>(Selector.Css("label[for='impressiveRadio']"));
    private RadioButtonElement No => ElementFactory.Create<RadioButtonElement>(Selector.Id("noRadio"));
    private CommonElement TextSuccess => ElementFactory.Create<CommonElement>(Selector.Class("text-success"));
    private RadioButtonElement Yes => ElementFactory.Create<RadioButtonElement>(Selector.Id("yesRadio"));
    private CommonElement YesLabel => ElementFactory.Create<CommonElement>(Selector.Css("label[for='yesRadio']"));

    /// <summary>
    /// Gets a value indicating whether the "Impressive" radio button is checked.
    /// </summary>
    /// <value>
    /// True if the "Impressive" radio button is checked; otherwise, false.
    /// </value>
    public bool IsImpressiveChecked
    {
        get
        {
            Log.Information("{PageName}: Retrieving a value whether the `Impressive` radio button is checked.", nameof(RadioButtonPage));
            
            bool isImpressiveChecked = Impressive.IsChecked;
            Log.Debug("{PageName}: isImpressiveChecked = `{IsImpressiveChecked}`", nameof(RadioButtonPage), isImpressiveChecked);
            return isImpressiveChecked;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the "No" radio button is disabled.
    /// </summary>
    /// <value>
    /// True if the "No" radio button is disabled; otherwise, false.
    /// </value>
    public bool IsNoDisabled
    {
        get
        {
            Log.Information("{PageName}: Retrieving a value indicating whether the `No` radio button is disabled.", nameof(RadioButtonPage));
            
            bool isNoDisabled = No.IsDisabled;
            Log.Debug("{PageName}: isNoDisabled = `{IsNoDisabled}`", nameof(RadioButtonPage), isNoDisabled);
            return isNoDisabled;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the "Yes" radio button is checked.
    /// </summary>
    /// <value>
    /// True if the "Yes" radio button is checked; otherwise, false.
    /// </value>
    public bool IsYesChecked
    {
        get
        {
            Log.Information("{PageName}: Retrieving a value whether the `Yes` radio button is checked.", nameof(RadioButtonPage));

            bool isYesChecked = Yes.IsChecked;
            Log.Debug("{PageName}: isYesChecked = `{IsYesChecked}`", nameof(RadioButtonPage), isYesChecked);
            return isYesChecked;
        }
    }

    /// <summary>
    /// Gets the text displayed for the currently selected radio button.
    /// </summary>
    /// <value>
    /// A string representing the text of the selected radio button.
    /// </value>
    public string SelectedRadioButtonText
    {
        get
        {
            Log.Information("{PageName}: Retrieving the text displayed for the currently selected radio button.", nameof(RadioButtonPage));
            
            string selectedRadioButtonText = TextSuccess.Text;
            Log.Debug("{PageName}: selectedRadioButtonText = `{SelectedRadioButtonText}`", nameof(RadioButtonPage), selectedRadioButtonText);
            return selectedRadioButtonText;
        }
    }

    /// <summary>
    /// Checks the "Impressive" radio button.
    /// </summary>
    public void CheckImpressive()
    {
        Log.Information("{PageName}: Checking `Impressive` radio button.", nameof(RadioButtonPage));

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
        Log.Information("{PageName}: Checking `Yes` radio button.", nameof(RadioButtonPage));
        
        if (!Yes.IsChecked)
        {
            YesLabel.Click();
        }
    }
}