using Karpatium.Core.TestData;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

[TestFixture]
public class TextBoxFixture : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void TextBoxOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.TextBoxPage}");
        RemoveBannerAndFooter();
    }
    
    [Test]
    public void EnsureThatAllDataIsCorrectlySubmitted()
    {
        string fullName = TestDataGenerator.GetFullName();
        string email = TestDataGenerator.GetEmail();
        string permanentAddress = TestDataGenerator.GetFullAddress();
        string currentAddress = TestDataGenerator.GetFullAddress();
        
        ToolsQaPages.TextBoxPage.SetFullName(fullName);
        ToolsQaPages.TextBoxPage.SetEmail(email);
        ToolsQaPages.TextBoxPage.SetPermanentAddress(permanentAddress);
        ToolsQaPages.TextBoxPage.SetCurrentAddress(currentAddress);
        ToolsQaPages.TextBoxPage.ClickSubmit();
        
        string actualFullName = ToolsQaPages.TextBoxPage.GetSubmittedFullName();
        string actualEmail = ToolsQaPages.TextBoxPage.GetSubmittedEmail();
        string actualPermanentAddress = ToolsQaPages.TextBoxPage.GetSubmittedPermanentAddress();
        string actualCurrentAddress = ToolsQaPages.TextBoxPage.GetSubmittedCurrentAddress();
        Assert.Multiple(() =>
        {
            Assert.That(actualFullName, Is.EqualTo(fullName), "Full name is incorrect.");
            Assert.That(actualEmail, Is.EqualTo(email), "Email is incorrect.");
            Assert.That(actualPermanentAddress, Is.EqualTo(permanentAddress), "Permanent address is incorrect.");
            Assert.That(actualCurrentAddress, Is.EqualTo(currentAddress), "Current address is incorrect.");
        });
    }
}