using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

public class LinksFixture : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void LinksOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.LinksPage}");
    }
    
    [Test, Order(1)]
    public void EnsureThatCreateLinkReturns201()
    {
        ToolsQaPages.LinksPage.ClickCreated();
        
        Assert.That(ToolsQaPages.LinksPage.LinkResponseText, Is.EqualTo(ToolsQaConstants.LinksPage.LinkResponse201), "Incorrect response.");
    }

    [Test, Order(2)]
    public void EnsureThatNotFoundLinkReturns404()
    {
        ToolsQaPages.LinksPage.ClickNotFound();
        
        Assert.That(ToolsQaPages.LinksPage.LinkResponseText, Is.EqualTo(ToolsQaConstants.LinksPage.LinkResponse404), "Incorrect response.");
    }
    
    [Test, Order(3)]
    public void EnsureThatHomeLinkOpensNewPage()
    {
        ToolsQaPages.LinksPage.ClickHome();
        WebManager.Browser.SwitchToChildTab();
        
        Assert.That(WebManager.Browser.Url, Is.EqualTo(ToolsQaConstants.LinksPage.LinkTabUrl), "Incorrect url.");
    }
}