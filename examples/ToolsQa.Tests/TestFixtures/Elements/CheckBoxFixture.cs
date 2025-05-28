using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestFixtures.Elements;

[TestFixture]
public class CheckBoxFixture : BaseFixture<CheckBoxTestData>
{
    protected override string TestDataPath => "TestData/Elements/CheckBoxData.json";
    
    [SetUp]
    public void CheckBoxSetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.CheckBoxPage}");
    }
    
    [Test] 
    public void EnsureThatMultipleNodesAreSelected()
    {
        IReadOnlyList<string> nodeNames = [TestData.MultipleNodesName1.ToLowerInvariant(), TestData.MultipleNodesName2.ToLowerInvariant() ];
        
        ToolsQaPages.CheckBoxPage.ClickExpandAll();
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName1);
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName2);

        IReadOnlyList<string> selectedNodeNames = ToolsQaPages.CheckBoxPage.GetSelectedNodes();
        Assert.That(selectedNodeNames, Is.EquivalentTo(nodeNames), "Incorrect selected nodes.");
    }

    [Test] 
    public void EnsureThatSingleNodeIsSelected()
    {
        ToolsQaPages.CheckBoxPage.ClickExpandAll();
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.SingleNodeName);

        IReadOnlyList<string> selectedNodeNames = ToolsQaPages.CheckBoxPage.GetSelectedNodes();
        Assert.That(selectedNodeNames, Has.Exactly(1).EqualTo(TestData.SingleNodeName.ToLowerInvariant()), "Incorrect selected node.");
    }
}

[Serializable]
public sealed class CheckBoxTestData
{
    public required string MultipleNodesName1 { get; init; }
    public required string MultipleNodesName2 { get; init; }
    public required string SingleNodeName { get; init; }
}