using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures;

[TestFixture]
public class CheckBoxFixture : BaseFixture<CheckBoxTestData>
{
    protected override string TestDataPath => "TestData/CheckBoxData.json";
    
    [SetUp]
    public void CheckBoxOneTimeSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.CheckBoxPage}");
        RemoveBannerAndFooter();
    }
    
    [Test] 
    public void VerifyThatMultipleNodesAreSelected()
    {
        IReadOnlyList<string> nodeNames = 
            [ TestData.MultipleNodesName1.ToLowerInvariant(), TestData.MultipleNodesName2.ToLowerInvariant() ];
        
        ToolsQaPages.CheckBoxPage.ClickExpandAll();
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName1);
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName2);

        IReadOnlyList<string> selectedNodeNames = ToolsQaPages.CheckBoxPage.GetSelectedNodes();
        Assert.That(selectedNodeNames, Is.EquivalentTo(nodeNames));
    }

    [Test] 
    public void VerifyThatSingleNodeIsSelected()
    {
        ToolsQaPages.CheckBoxPage.ClickExpandAll();
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.SingleNodeName);

        IReadOnlyList<string> selectedNodeNames = ToolsQaPages.CheckBoxPage.GetSelectedNodes();
        Assert.That(selectedNodeNames, Has.Exactly(1).EqualTo(TestData.SingleNodeName.ToLowerInvariant()));
    }
}

[Serializable]
public sealed class CheckBoxTestData
{
    public required string MultipleNodesName1 { get; init; }
    public required string MultipleNodesName2 { get; init; }
    public required string SingleNodeName { get; init; }
}