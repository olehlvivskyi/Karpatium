using Karpatium.Core.Utilities;
using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;

namespace ToolsQa.UI.Components.React;

/// <summary>
/// Represents a React table component.
/// </summary>
internal class ReactTableComponent : BaseComponent
{
    private readonly List<string> _headers;
    
    internal ReactTableComponent(Element componentWrapper) : base(componentWrapper)
    {
        _headers = ConditionalWaiter.ForResult(() => Headers.Select(header => header.Text).ToList(), $"{nameof(ReactTableComponent)}: {nameof(ReactTableComponent)} failed.");
    }
    
    private ElementCollection<CommonElement> Headers => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("rt-th"), ComponentWrapper);
    private ElementCollection<CommonElement> Rows => ElementFactory.CreateMultiple<CommonElement>(Selector.Class("rt-tr-group"), ComponentWrapper);
    
    internal ReactTableRowComponent? GetRow(string headerText, string cellText)
    {
        IReadOnlyList<ReactTableRowComponent> rows = GetRows();
        ReactTableRowComponent? row = rows.FirstOrDefault(row => row.GetCellText(headerText) == cellText);
        
        return row;
    }
    
    private IReadOnlyList<ReactTableRowComponent> GetRows()
    {
        List<ReactTableRowComponent> rows = ConditionalWaiter.ForResult(() => Rows.Select(row => new ReactTableRowComponent(row, _headers)).ToList(), $"{nameof(ReactTableComponent)}: {nameof(GetRows)} failed.");

        return rows;
    }
}