using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Links")]
[TestFixture]
public sealed class LinksTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.LinksPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that \"Created\" link returns 201."), Order(1)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatCreateLinkReturns201()
    {
        ToolsQaPages.LinksPage.ClickCreated();
        
        Assert.That(ToolsQaPages.LinksPage.LinkResponseText, Is.EqualTo(ToolsQaConstants.LinksPage.LinkResponse201), "Incorrect response.");
    }

    [TestCase(TestName = "Ensure that \"Not Found\" link returns 404."), Order(2)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatNotFoundLinkReturns404()
    {
        ToolsQaPages.LinksPage.ClickNotFound();
        
        Assert.That(ToolsQaPages.LinksPage.LinkResponseText, Is.EqualTo(ToolsQaConstants.LinksPage.LinkResponse404), "Incorrect response.");
    }
    
    [TestCase(TestName = "Ensure that \"Home\" link opens new page."), Order(3)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatHomeLinkOpensNewPage()
    {
        ToolsQaPages.LinksPage.ClickHome();
        WebManager.Browser.SwitchToChildTab();
        
        Assert.That(WebManager.Browser.Url, Is.EqualTo(ToolsQaConstants.LinksPage.LinkTabUrl), "Incorrect url.");
    }
}