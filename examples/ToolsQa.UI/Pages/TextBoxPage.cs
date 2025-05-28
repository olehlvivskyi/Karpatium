using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Text Box" page.
/// </summary>
public sealed class TextBoxPage(string relativePath) : BasePage(relativePath)
{
    private const string PageName = nameof(TextBoxPage);
    
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
    /// Clicks on the "Submit" button.
    /// </summary>
    public void ClickSubmit()
    {
        Log.Information("{PageName}: Clicking on the \"Submit\" button.", PageName);
        
        Submit.Click();
    }
    
    /// <summary>
    /// Gets the submitted current address.
    /// </summary>
    public string GetSubmittedCurrentAddress()
    {
        Log.Information("{PageName}: Checking the submitted current address.", PageName);
        
        string currentAddress = CurrentAddressOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(GetSubmittedCurrentAddress), currentAddress);
        
        return currentAddress;
    }

    /// <summary>
    /// Gets the submitted full name.
    /// </summary>
    public string GetSubmittedFullName()
    {
        Log.Information("{PageName}: Checking the submitted full name.", PageName);
        
        string fullName = FullNameOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(GetSubmittedFullName), fullName);
        
        return fullName;
    }

    /// <summary>
    /// Gets the submitted email address.
    /// </summary>
    public string GetSubmittedEmail()
    {
        Log.Information("{PageName}: Checking the submitted email address.", PageName);
        
        string email = EmailOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(GetSubmittedEmail), email);
        
        return email;
    }

    /// <summary>
    /// Gets the submitted permanent address.
    /// </summary>
    public string GetSubmittedPermanentAddress()
    {
        Log.Information("{PageName}: Checking the submitted permanent address.", PageName);
        
        string permanentAddress = PermanentAddressOutput.Text.Split(':')[1];
        Log.Debug("{PageName}: {MemberName} = `{MemberValue}`", PageName, nameof(GetSubmittedPermanentAddress), permanentAddress);
        
        return permanentAddress;
    }

    /// <summary>
    /// Sets value for "Current Address" field.
    /// </summary>
    /// <param name="currentAddress">The text to set as the current address.</param>
    public void SetCurrentAddress(string currentAddress)
    {
        Log.Information("{PageName}: Setting `{CurrentAddress}` for \"Current Address\" field.", PageName, currentAddress);
        
        CurrentAddressInput.SetText(currentAddress);
    }

    /// <summary>
    /// Sets value for "Email" field.
    /// </summary>
    /// <param name="email">The text to set as the email address.</param>
    public void SetEmail(string email)
    {
        Log.Information("{PageName}: Setting `{Email}` for \"Email\" field.", PageName, email);
        
        EmailInput.SetText(email);
    }

    /// <summary>
    /// Sets value for "Full Name" field.
    /// </summary>
    /// <param name="fullName">The text to set as the full name.</param>
    public void SetFullName(string fullName)
    {
        Log.Information("{PageName}: Setting `{FullName}` for \"Full Name\" field.", PageName, fullName);
        
        FullNameInput.SetText(fullName);
    }

    /// <summary>
    /// Sets value for "Permanent Address" field.
    /// </summary>
    /// <param name="permanentAddress">The text to set as the permanent address.</param>
    public void SetPermanentAddress(string permanentAddress)
    {
        Log.Information("{PageName}: Setting `{PermanentAddress}` for \"Permanent Address\" field.", PageName, permanentAddress);
        
        PermanentAddressInput.SetText(permanentAddress);
    }
}