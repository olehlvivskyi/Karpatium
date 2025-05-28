using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Check Box" page.
/// </summary>
public sealed class CheckBoxPage(string relativePath) : BasePage(relativePath)
{
    private const string PageName = nameof(CheckBoxPage);
    
    private CommonElement ExpandAll => ElementFactory.Create<CommonElement>(Selector.Class("rct-option-expand-all"));
    private CommonElement GetNodeCheckbox(CommonElement node) => ElementFactory.Create<CommonElement>(Selector.Class("rct-checkbox"), node);
    private ElementCollection<CommonElement> Nodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("rct-node-leaf"));
    private ElementCollection<CommonElement> SelectedNodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("text-success"));

    /// <summary>
    /// Clicks on the "Expand All" button.
    /// </summary>
    public void ClickExpandAll()
    {
        Log.Information("{PageName}: Clicking on the \"Expand All\" button.", PageName);
        
        ExpandAll.Click();
    }

    /// <summary>
    /// Retrieves the names of the selected nodes.
    /// </summary>
    /// <returns>A read-only list of selected node names.</returns>
    public IReadOnlyList<string> GetSelectedNodes()
    {
        Log.Information("{PageName}: Checking the names of the selected nodes.", PageName);

        IReadOnlyList<string> selectedNodes = SelectedNodes
            .Select(node => node.Text)
            .ToList();
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
        
        CommonElement nodeToSelect = Nodes.First(node => node.Text.Contains(nodeName));
        CommonElement nodeCheckBox = GetNodeCheckbox(nodeToSelect);
        
        nodeCheckBox.Click();
    }
}