using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

[TestFixture]
public class RadioButtonFixture : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [SetUp]
    public void RadioButtonSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.RadioButtonPage}");
    }
    
    [Test]
    public void EnsureThatNoRadioButtonIsDisabled()
    {
        Assert.That(ToolsQaPages.RadioButtonPage.IsNoEnabled, Is.False, "\"No\" radio button is not disabled.");
    }

    [Test]
    public void EnsureThatYesRadioButtonIsChecked()
    {
        ToolsQaPages.RadioButtonPage.CheckYes();

        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.RadioButtonPage.IsYesChecked, Is.True, "\"Yes\" radio button is not checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.SelectedRadioButtonText, Is.EqualTo(ToolsQaConstants.RadioButtonPage.YesRadioButtonText), "`Yes` radio button is not checked.");
        });
    }
    
    [Test]
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