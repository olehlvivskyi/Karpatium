using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

public class ButtonsFixture : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void RadioButtonOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.ButtonsPage}");
    }
    
    [Test]
    public void EnsureThatClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.ClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.ClickMeMessageText), "'Click Me' button does not work.");
    }
    
    [Test]
    public void EnsureThatDoubleClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickDoubleClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.DoubleClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.DoubleClickMeMessageText), "'Double Click Me' button does not work.");
    }
    
    [Test]
    public void EnsureThatRightClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickRightClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.RightClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.RightClickMeMessageText), "'Right Click Me' button does not work.");
    }
}