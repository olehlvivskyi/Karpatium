using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;

namespace ToolsQa.UI.Components.React;

/// <summary>
/// Represents a table row component within a React-based table structure.
/// </summary>
internal sealed class ReactTableRowComponent(CommonElement componentWrapper, List<string> headers) : BaseComponent<CommonElement>(componentWrapper)
{
    private ElementCollection<CommonElement> Cells => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("rt-td"), ComponentWrapper);

    internal string GetCellText(string headerText)
    {
        int headerIndex = GetHeaderIndex(headerText);
        
        return Cells[headerIndex].Text;
    }
    
    private int GetHeaderIndex(string headerText) => headers.IndexOf(headerText);
}