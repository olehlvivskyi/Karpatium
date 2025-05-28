using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

public class DynamicPropertiesFixture : BaseFixture<EmptyTestData>
{
    private const int DomUpdateTimeoutInMilliseconds = 5000;
    
    protected override string TestDataPath => string.Empty;
    
    [OneTimeSetUp]
    public void DynamicPropertiesOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.DynamicPropertiesPage}");
    }
    
    [Test, Order(1)]
    public void EnsureThatInitialButtonPropertiesAreCorrect()
    {
        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsFirstButtonEnabled, Is.False, "\"Will enable 5 seconds\" button is enabled.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.SecondButtonTextColor, Is.EqualTo(ToolsQaConstants.DynamicPropertiesPage.InitialColorChangeTextColor), "Incorrect \"Color Change\" button text color.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsThirdButtonVisible, Is.False, "\"Visible after 5 seconds\" button is visible.");
        });
    }
    
    [Test, Order(2)]
    public void EnsureThatAfter5SecondsButtonPropertiesAreCorrect()
    {
        ConditionalWaiter.ForTrueIfPossible( () => ToolsQaPages.DynamicPropertiesPage.IsFirstButtonEnabled, DomUpdateTimeoutInMilliseconds);
        
        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsFirstButtonEnabled, Is.True, "\"Will enable 5 seconds\" button is not enabled.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.SecondButtonTextColor, Is.EqualTo(ToolsQaConstants.DynamicPropertiesPage.NextColorChangeTextColor), "Incorrect \"Color Change\" button text color.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsThirdButtonVisible, Is.True, "\"Visible after 5 seconds\" button is not visible.");
        });
    }
}