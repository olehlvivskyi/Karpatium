using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Check Box" page.
/// </summary>
public sealed class CheckBoxPage(string relativePath) : BasePage(relativePath)
{
    protected override string PageName => "\"Check Box\" page";
    
    private CommonElement GetNodeSwitcher(CommonElement node) => ElementFactory.Create<CommonElement>(Selector.Class("rc-tree-switcher"), node);
    private CommonElement GetNodeCheckbox(CommonElement node) => ElementFactory.Create<CommonElement>(Selector.Class("rc-tree-checkbox"), node);
    private ElementCollection<CommonElement> Nodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Css(".rc-tree-list .rc-tree-treenode"));
    private ElementCollection<CommonElement> SelectedNodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("text-success"));

    /// <summary>
    /// Expands a node by its name.
    /// </summary>
    /// <param name="nodeName">The name of the node to be expanded.</param>
    public void ExpandNode(string nodeName)
    {
        Log.Information("{PageName}: Expanding `{NodeName}` node.", PageName, nodeName);

        CommonElement nodeToSelect = ConditionalWaiter.ForResult(() => Nodes.First(node => node.Text.Contains(nodeName)), $"{PageName}: {nameof(SelectNode)} failed.");
        CommonElement nodeSwitcher = GetNodeSwitcher(nodeToSelect);

        nodeSwitcher.Click();
        WebManager.Waiter.ForPageSourceIsNotChanged();
    }

    /// <summary>
    /// Retrieves the names of the selected nodes.
    /// </summary>
    /// <returns>A read-only list of selected node names.</returns>
    public IReadOnlyList<string> GetSelectedNodes()
    {
        Log.Information("{PageName}: Checking the names of the selected nodes.", PageName);

        IReadOnlyList<string> selectedNodes = ConditionalWaiter.ForResult(() => SelectedNodes
            .Select(node => node.Text)
            .ToList(), $"{PageName}: {nameof(GetSelectedNodes)} failed.");
        Log.Debug("{PageName}: {MemberName} = `{@MemberValue}`", PageName, nameof(GetSelectedNodes), selectedNodes);

        return selectedNodes;
    }

    /// <summary>
    /// Selects a node by its name.
    /// </summary>
    /// <param name="nodeName">The name of the node to be selected.</param>
    public void SelectNode(string nodeName)
    {
        Log.Information("{PageName}: Selecting `{NodeName}` node.", PageName, nodeName);
        
        CommonElement nodeToSelect = ConditionalWaiter.ForResult(() => Nodes.First(node => node.Text.Contains(nodeName)), $"{PageName}: {nameof(SelectNode)} failed.");
        CommonElement nodeCheckBox = GetNodeCheckbox(nodeToSelect);
        
        nodeCheckBox.Click();
    }
}