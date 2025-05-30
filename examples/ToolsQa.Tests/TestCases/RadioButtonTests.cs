using Allure.NUnit.Attributes;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Radio Button")]
[TestFixture]
public class RadioButtonTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [SetUp]
    public void SetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.RadioButtonPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that \"No\" radio button is disabled.")]
    public void EnsureThatNoRadioButtonIsDisabled()
    {
        Assert.That(ToolsQaPages.RadioButtonPage.IsNoEnabled, Is.False, "\"No\" radio button is not disabled.");
    }

    [TestCase(TestName = "Ensure that \"Yes\" radio button is checked.")]
    public void EnsureThatYesRadioButtonIsChecked()
    {
        ToolsQaPages.RadioButtonPage.CheckYes();

        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.RadioButtonPage.IsYesChecked, Is.True, "\"Yes\" radio button is not checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.SelectedRadioButtonText, Is.EqualTo(ToolsQaConstants.RadioButtonPage.YesRadioButtonText), "`Yes` radio button is not checked.");
        });
    }
    
    [TestCase(TestName = "Ensure that \"Yes\" radio button is not checked after \"Impressive\" radio button is checked.")]
    public void EnsureThatYesRadioButtonIsNotCheckedAfterImpressiveRadioButtonIsChecked()
    {
        ToolsQaPages.RadioButtonPage.CheckYes();
        ToolsQaPages.RadioButtonPage.CheckImpressive();

        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.RadioButtonPage.IsYesChecked, Is.False, "\"Yes\" radio button is checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.IsImpressiveChecked, Is.True, "\"Impressive\" radio button is not checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.SelectedRadioButtonText, Is.EqualTo(ToolsQaConstants.RadioButtonPage.ImpressiveRadioButtonText), "Incorrect selected radio button text.");
        });
    }
}