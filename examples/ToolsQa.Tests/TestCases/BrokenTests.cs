using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Broken")]
[TestFixture]
public sealed class BrokenTests : BaseFixture<EmptyTestData>
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
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatLogo1ImageIsLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsFirstLogoImageValid, Is.True, "First logo image is not loaded.");
    }
    
    [TestCase(TestName = "Ensure that logo 2 image is not loaded.")]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatLogo2ImageIsNotLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsSecondLogoImageValid, Is.False, "Second logo image is loaded.");
    }
}