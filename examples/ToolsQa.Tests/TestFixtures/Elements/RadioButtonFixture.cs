using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

[TestFixture]
public class RadioButtonFixture : BaseFixture<RadioButtonTestData>
{
    protected override string TestDataPath => "TestData/Elements/RadioButtonData.json";
    
    [SetUp]
    public void RadioButtonSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.RadioButtonPage}");
        RemoveBannerAndFooter();
    }
    
    [Test]
    public void EnsureThatNoRadioButtonIsDisabled()
    {
        Assert.That(ToolsQaPages.RadioButtonPage.IsNoDisabled, Is.True, "`No` radio button is not disabled.");
    }

    [Test]
    public void EnsureThatYesRadioButtonIsChecked()
    {
        ToolsQaPages.RadioButtonPage.CheckYes();

        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.RadioButtonPage.IsYesChecked, Is.True, "`Yes` radio button is not checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.SelectedRadioButtonText, Is.EqualTo(TestData.YesText), "`Yes` radio button is not checked.");
        });
    }
    
    [Test]
    public void EnsureThatYesRadioButtonIsNotCheckedAfterImpressiveRadioButtonIsChecked()
    {
        ToolsQaPages.RadioButtonPage.CheckYes();
        ToolsQaPages.RadioButtonPage.CheckImpressive();

        Assert.Multiple(() =>
        {
            Assert.That(ToolsQaPages.RadioButtonPage.IsYesChecked, Is.False, "`Yes` radio button is checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.IsImpressiveChecked, Is.True, "`Impressive` radio button is not checked.");
            Assert.That(ToolsQaPages.RadioButtonPage.SelectedRadioButtonText, Is.EqualTo(TestData.ImpressiveText), "`Impressive` radio button is not checked.");
        });
    }
}

[Serializable]
public sealed class RadioButtonTestData
{
    public required string ImpressiveText { get; init; }
    public required string YesText { get; init; }
}