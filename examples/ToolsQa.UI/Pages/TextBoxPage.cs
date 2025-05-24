using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Represents the "TextBox" page.
/// </summary>
public sealed class TextBoxPage(string relativePath) : BasePage(relativePath)
{
    private InputElement CurrentAddressInput => ElementFactory.Create<InputElement>(Selector.Id("currentAddress"));
    private CommonElement CurrentAddressOutput => ElementFactory.Create<CommonElement>(Selector.Css("p#currentAddress"));
    private InputElement EmailInput => ElementFactory.Create<InputElement>(Selector.Id("userEmail"));
    private CommonElement EmailOutput => ElementFactory.Create<CommonElement>(Selector.Id("email"));
    private InputElement FullNameInput => ElementFactory.Create<InputElement>(Selector.Id("userName"));
    private CommonElement FullNameOutput => ElementFactory.Create<CommonElement>(Selector.Id("name"));
    private InputElement PermanentAddressInput => ElementFactory.Create<InputElement>(Selector.Id("permanentAddress"));
    private CommonElement PermanentAddressOutput => ElementFactory.Create<CommonElement>(Selector.Css("p#permanentAddress"));
    private CommonElement Submit => ElementFactory.Create<CommonElement>(Selector.Id("submit"));

    /// <summary>
    /// Retrieves the submitted current address.
    /// </summary>
    /// <returns>The submitted current address as a string.</returns>
    public string GetSubmittedCurrentAddress()
    {
        Log.Information("{PageName}: Retrieving the submitted current address.", nameof(TextBoxPage));
        
        string currentAddress = CurrentAddressOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: currentAddress = `{CurrentAddress}`", 
            nameof(TextBoxPage), currentAddress);
        
        return currentAddress;
    }

    /// <summary>
    /// Retrieves the submitted full name.
    /// </summary>
    /// <returns>The submitted full name as a string.</returns>
    public string GetSubmittedFullName()
    {
        Log.Information("{PageName}: Retrieving the submitted full name.", nameof(TextBoxPage));
        
        string fullName = FullNameOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: fullName = `{FullName}`", 
            nameof(TextBoxPage), fullName);
        
        return fullName;
    }

    /// <summary>
    /// Retrieves the submitted email address.
    /// </summary>
    /// <returns>The submitted email address as a string.</returns>
    public string GetSubmittedEmail()
    {
        Log.Information("{PageName}: Retrieving the submitted email address.", nameof(TextBoxPage));
        
        string email = EmailOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: email = `{Email}`", 
            nameof(TextBoxPage), email);
        
        return email;
    }

    /// <summary>
    /// Retrieves the submitted permanent address.
    /// </summary>
    /// <returns>The submitted permanent address as a string.</returns>
    public string GetSubmittedPermanentAddress()
    {
        Log.Information("{PageName}: Retrieving the submitted permanent address.", nameof(TextBoxPage));
        
        string permanentAddress = PermanentAddressOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: permanentAddress = `{PermanentAddress}`",
            nameof(TextBoxPage), permanentAddress);
        
        return permanentAddress;
    }
    
    /// <summary>
    /// Clicks "Submit" button.
    /// </summary>
    public void ClickSubmit()
    {
        Log.Information("{PageName}: Clicking `Submit` button.", nameof(TextBoxPage));
        
        Submit.Click();
    }

    /// <summary>
    /// Sets the specified current address.
    /// </summary>
    /// <param name="currentAddress">The text to set as the current address.</param>
    public void SetCurrentAddress(string currentAddress)
    {
        Log.Information("{PageName}: Setting `{CurrentAddress}` current address.",
            nameof(TextBoxPage), currentAddress);
        
        CurrentAddressInput.SetText(currentAddress);
    }

    /// <summary>
    /// Sets the specified email address.
    /// </summary>
    /// <param name="email">The text to set as the email address.</param>
    public void SetEmail(string email)
    {
        Log.Information("{PageName}: Setting `{Email}` email address.",
            nameof(TextBoxPage), email);
        
        EmailInput.SetText(email);
    }

    /// <summary>
    /// Sets the specified full name.
    /// </summary>
    /// <param name="fullName">The text to set as the full name.</param>
    public void SetFullName(string fullName)
    {
        Log.Information("{PageName}: Setting `{FullName}` full name.",
            nameof(TextBoxPage), fullName);
        
        FullNameInput.SetText(fullName);
    }

    /// <summary>
    /// Sets the specified permanent address.
    /// </summary>
    /// <param name="permanentAddress">The text to set as the permanent address.</param>
    public void SetPermanentAddress(string permanentAddress)
    {
        Log.Information("{PageName}: Setting `{PermanentAddress}` permanent address.",
            nameof(TextBoxPage), permanentAddress);
        
        PermanentAddressInput.SetText(permanentAddress);
    }
}