using Allure.NUnit.Attributes;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Check Box")]
[TestFixture]
public sealed class CheckBoxTests : BaseFixture<CheckBoxTestData>
{
    protected override string TestDataPath => "TestData/CheckBoxData.json";
    
    [AllureBefore]
    [SetUp]
    public void SetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.CheckBoxPage}");
        RemoveBanners();
    }
    
    [TestCase(TestName = "Ensure that multiple nodes are selected.")]
    [RetryOnErrorAndFailure(3)]
    public void EnsureThatMultipleNodesAreSelected()
    {
        IReadOnlyList<string> nodeNames = [TestData.MultipleNodesName1.ToLowerInvariant(), TestData.MultipleNodesName2.ToLowerInvariant() ];
        
        ToolsQaPages.CheckBoxPage.ClickExpandAll();
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName1);
        ToolsQaPages.CheckBoxPage.SelectNode(TestData.MultipleNodesName2);

        IReadOnlyList<string> selectedNodeNames = ToolsQaPages.CheckBoxPage.GetSelectedNodes();
        Assert.That(selectedNodeNames, Is.EquivalentTo(nodeNames), "Incorrect selected nodes.");
    }

    [TestCase(TestName = "Ensure that single node is selected.")]
    [RetryOnErrorAndFailure(3)]
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