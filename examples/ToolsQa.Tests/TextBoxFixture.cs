using Karpatium.Core.TestData;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests;

/*
 * The ToolsQa.Tests project is currently in its initial draft stage, created primarily to validate the framework
 * and page object structure.
 * At this stage, it lacks configuration, structured layering, and additional functionality, which will be introduced
 * in upcoming commits.
 * Stay tuned for further updates and improvements.
*/
[TestFixture]
public class TextBoxFixture : BaseFixture
{
    [OneTimeSetUp]
    public void TextBoxOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo("https://demoqa.com" + ToolsQaPageUrls.TextBoxPage);
        
        WebManager.Browser.JavaScript.Execute("document.querySelector('#fixedban').remove();");
        WebManager.Browser.JavaScript.Execute("document.querySelector('footer').remove();");
    }
    
    [Test]
    public void VerifyThatAllDataIsCorrectlySubmitted()
    {
        var fullName = TestDataGenerator.GetFullName();
        var email = TestDataGenerator.GetEmail();
        var permanentAddress = TestDataGenerator.GetFullAddress();
        var currentAddress = TestDataGenerator.GetFullAddress();
        
        ToolsQaPages.TextBoxPage.SetFullName(fullName);
        ToolsQaPages.TextBoxPage.SetEmail(email);
        ToolsQaPages.TextBoxPage.SetPermanentAddress(permanentAddress);
        ToolsQaPages.TextBoxPage.SetCurrentAddress(currentAddress);
        ToolsQaPages.TextBoxPage.ClickSubmit();
        
        var actualFullName = ToolsQaPages.TextBoxPage.GetSubmittedFullName();
        var actualEmail = ToolsQaPages.TextBoxPage.GetSubmittedEmail();
        var actualPermanentAddress = ToolsQaPages.TextBoxPage.GetSubmittedPermanentAddress();
        var actualCurrentAddress = ToolsQaPages.TextBoxPage.GetSubmittedCurrentAddress();
        Assert.Multiple(() =>
        {
            Assert.That(actualFullName, Is.EqualTo(fullName), "Full name is incorrect.");
            Assert.That(actualEmail, Is.EqualTo(email), "Email is incorrect.");
            Assert.That(actualPermanentAddress, Is.EqualTo(permanentAddress), "Permanent address is incorrect.");
            Assert.That(actualCurrentAddress, Is.EqualTo(currentAddress), "Current address is incorrect.");
        });
    }
}