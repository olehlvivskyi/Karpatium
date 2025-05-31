using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Dynamic Properties")]
[TestFixture]
public sealed class DynamicPropertiesTests : BaseFixture<EmptyTestData>
{
    private const int DomUpdateTimeInMilliseconds = 5000;
    
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.DynamicPropertiesPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that initial button properties are correct."), Order(1)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatInitialButtonPropertiesAreCorrect()
    {
        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsFirstButtonEnabled, Is.False, "\"Will enable 5 seconds\" button is enabled.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.SecondButtonTextColor, Does.Contain(ToolsQaConstants.DynamicPropertiesPage.InitialColorChangeTextColor), "Incorrect \"Color Change\" button text color.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsThirdButtonVisible, Is.False, "\"Visible after 5 seconds\" button is visible.");
        });
    }
    
    [TestCase(TestName = "Ensure that after 5 seconds button properties are correct."), Order(2)]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatAfter5SecondsButtonPropertiesAreCorrect()
    {
        Thread.Sleep(DomUpdateTimeInMilliseconds);
        
        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsFirstButtonEnabled, Is.True, "\"Will enable 5 seconds\" button is not enabled.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.SecondButtonTextColor, Does.Contain(ToolsQaConstants.DynamicPropertiesPage.NextColorChangeTextColor), "Incorrect \"Color Change\" button text color.");
            Assert.That(ToolsQaPages.DynamicPropertiesPage.IsThirdButtonVisible, Is.True, "\"Visible after 5 seconds\" button is not visible.");
        });
    }
}