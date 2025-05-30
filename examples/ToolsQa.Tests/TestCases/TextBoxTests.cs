using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Text Box")]
[TestFixture]
public class TextBoxTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.TextBoxPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that form is submitted.")]
    [AllureEpic("TQA-123 Elements: Implement Elements subsection")]
    [AllureLink("Epic", "TQA-123")]
    [AllureStory("TQA-456 Text Box: Implement page")]
    [AllureLink("User Story", "TQA-456")]
    [AllureTms("Test Case", "TC-12345")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("oleh.lvivskyi")]
    public void EnsureThatFormIsSubmitted()
    {
        string fullName = Faker.Name.FullName();
        string email = Faker.Internet.Email();
        string permanentAddress = Faker.Address.FullAddress();
        string currentAddress = Faker.Address.FullAddress();
        
        ToolsQaPages.TextBoxPage.SetFullName(fullName);
        ToolsQaPages.TextBoxPage.SetEmail(email);
        ToolsQaPages.TextBoxPage.SetCurrentAddress(currentAddress);
        ToolsQaPages.TextBoxPage.SetPermanentAddress(permanentAddress);
        ToolsQaPages.TextBoxPage.ClickSubmit();
        
        string actualFullName = ToolsQaPages.TextBoxPage.GetSubmittedFullName();
        string actualEmail = ToolsQaPages.TextBoxPage.GetSubmittedEmail();
        string actualCurrentAddress = ToolsQaPages.TextBoxPage.GetSubmittedCurrentAddress();
        string actualPermanentAddress = ToolsQaPages.TextBoxPage.GetSubmittedPermanentAddress();
        Assert.Multiple(() =>
        {
            Assert.That(actualFullName, Is.EqualTo(fullName), "Incorrect full name.");
            Assert.That(actualEmail, Is.EqualTo(email), "Incorrect email.");
            Assert.That(actualCurrentAddress, Is.EqualTo(currentAddress), "Incorrect current address.");
            Assert.That(actualPermanentAddress, Is.EqualTo(permanentAddress), "Incorrect permanent address.");
        });
    }
}