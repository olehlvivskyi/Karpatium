using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Buttons")]
[TestFixture]
public sealed class ButtonsTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.ButtonsPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that \"Click Me\" button works.")]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.ClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.ClickMeMessageText), "\"Click Me\" button does not work.");
    }
    
    [TestCase(TestName = "Ensure that \"Double Click Me\" button works.")]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatDoubleClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickDoubleClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.DoubleClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.DoubleClickMeMessageText), "\"Double Click Me\" button does not work.");
    }
    
    [TestCase(TestName = "Ensure that \"Right Click Me\" button works.")]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatRightClickMeButtonWorks()
    {
        ToolsQaPages.ButtonsPage.ClickRightClickMe();
        
        Assert.That(ToolsQaPages.ButtonsPage.RightClickMeMessageText, Is.EqualTo(ToolsQaConstants.ButtonsPage.RightClickMeMessageText), "\"Right Click Me\" button does not work.");
    }
}