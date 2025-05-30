using Allure.NUnit.Attributes;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Broken")]
[TestFixture]
public class BrokenTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.BrokenPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that logo 1 image is loaded.")]
    public void EnsureThatLogo1ImageIsLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsFirstLogoImageValid, Is.True, "First logo image is not loaded.");
    }
    
    [TestCase(TestName = "Ensure that logo 2 image is not loaded.")]
    public void EnsureThatLogo2ImageIsNotLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsSecondLogoImageValid, Is.False, "Second logo image is loaded.");
    }
}