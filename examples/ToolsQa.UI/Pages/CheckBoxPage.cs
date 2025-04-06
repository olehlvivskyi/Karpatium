using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Represents the "CheckBox" page.
/// </summary>
public class CheckBoxPage(string relativePath) : BasePage(relativePath)
{
    private CommonElement ExpandAll => ElementFactory.Create<CommonElement>(Selector.Class("rct-option-expand-all"));
    private ElementCollection<CommonElement> Nodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("rct-node-leaf"));
    private ElementCollection<CommonElement> SelectedNodes => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("text-success"));

    /// <summary>
    /// Clicks "Expand All" button.
    /// </summary>
    public void ClickExpandAll()
    {
        Log.Information("{PageName}: Clicking `Expand All` button.", nameof(CheckBoxPage));
        
        ExpandAll.Click();
    }

    /// <summary>
    /// Retrieves the names of the selected nodes.
    /// </summary>
    /// <returns>A read-only list of selected node names.</returns>
    public IReadOnlyList<string> GetSelectedNodes()
    {
        Log.Information("{PageName}: Retrieving the names of the selected nodes.", nameof(CheckBoxPage));

        IReadOnlyList<string> selectedNodes = SelectedNodes
            .Select(node => node.Text)
            .ToList();
        Log.Debug("{PageName}: selectedNodes = `{@SelectedNodes}`", 
            nameof(CheckBoxPage), selectedNodes);

        return selectedNodes;
    }

    /// <summary>
    /// Selects a node by its name.
    /// </summary>
    /// <param name="nodeName">The name of the node to be selected.</param>
    public void SelectNode(string nodeName)
    {
        Log.Information("{PageName}: Selecting `{NodeName}` node.",
            nameof(CheckBoxPage), nodeName);
        
        CommonElement nodeToSelect = Nodes.First(node => node.Text.Contains(nodeName));
        CommonElement nodeCheckBox = ElementFactory.Create<CommonElement>(Selector.Class("rct-checkbox"), nodeToSelect);
        
        nodeCheckBox.Click();
    }
}