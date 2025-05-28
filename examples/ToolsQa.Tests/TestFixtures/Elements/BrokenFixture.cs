using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

public class BrokenFixture : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void LinksOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.BrokenPage}");
    }
    
    [Test]
    public void EnsureThatLogo1ImageIsLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsFirstLogoImageValid, Is.True, "First logo image is not loaded.");
    }
    
    [Test]
    public void EnsureThatLogo2ImageIsNotLoaded()
    {
        Assert.That(ToolsQaPages.BrokenPage.IsSecondLogoImageValid, Is.False, "Second logo image is loaded.");
    }
}