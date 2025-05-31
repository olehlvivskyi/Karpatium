using Karpatium.Core.Attributes;
using Karpatium.Core.Extensions;
using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;
using ToolsQa.UI.Components.React;

namespace ToolsQa.UI.Layouts;

/// <summary>
/// Provides access to the "Worker" table.
/// </summary>
public sealed class WorkerTableLayout(CommonElement layoutWrapper, string pageName) : BaseLayout<CommonElement>(layoutWrapper, pageName)
{
    private readonly ReactTableComponent _tableComponent = new(layoutWrapper);
    
    protected override string LayoutName => "\"Worker\" table";

    /// <summary>
    /// Gets a specific row where the cell value under a given header matches the specified text.
    /// </summary>
    /// <param name="header">The header text of the column to search in.</param>
    /// <param name="cellText">The cell text to match under the specified header column.</param>
    public WorkerTableRowItem? GetRow(WorkerTableHeader header, string cellText)
    {
        Log.Information("{PageName}[{LayoutName}]: Checking row where `{HeaderText}` header has `{CellText}` cell text.", PageName, LayoutName, header, cellText);

        string headerText = header.GetStringValue();
        ReactTableRowComponent? row = _tableComponent.GetRow(headerText, cellText);
        Log.Debug("{PageName}[{LayoutName}]: row = `{Row}`", PageName, LayoutName, row?.ComponentWrapper.Text.Replace("\n", " | "));
        
        return row == null 
            ? null 
            : new WorkerTableRowItem(row, PageName, LayoutName);
    }
}

/// <summary>
/// Provides access to the "Worker" table row.
/// </summary>
public class WorkerTableRowItem
{
    private readonly ReactTableRowComponent _rowComponent;
    private readonly string _pageName;
    private readonly string _layoutName;

    internal WorkerTableRowItem(ReactTableRowComponent rowComponent, string pageName, string layoutName)
    {
        _rowComponent = rowComponent;
        _pageName = pageName;
        _layoutName = layoutName;
    }
    
    private CommonElement Delete => ElementFactory.Create<CommonElement>(Selector.Css("[title='Delete']"), _rowComponent.ComponentWrapper);
    private CommonElement Edit => ElementFactory.Create<CommonElement>(Selector.Css("[title='Edit']"), _rowComponent.ComponentWrapper);

    /// <summary>
    /// Clicks on the "Delete" icon.
    /// </summary>
    public void ClickDelete()
    {
        Log.Information("{PageName}[{LayoutName}]: Clicking on the \"Delete\" icon.", _pageName, _layoutName);
        
        Delete.Click();
    }

    /// <summary>
    /// Clicks on the "Edit" icon.
    /// </summary>
    public void ClickEdit()
    {
        Log.Information("{PageName}[{LayoutName}]: Clicking on the \"Edit\" icon.", _pageName, _layoutName);
        
        Edit.Click();
    }

    /// <summary>
    /// Gets the text content of a cell in the row based on the specified header.
    /// </summary>
    /// <param name="header">The header of the column whose cell text is to be retrieved.</param>
    public string GetCellText(WorkerTableHeader header)
    {
        Log.Information("{PageName}[{LayoutName}]: Checking cell text for \"{Header}\" header.", _pageName, _layoutName, header);
        
        string headerText = header.GetStringValue();
        string cellText = _rowComponent.GetCellText(headerText);
        Log.Debug("{PageName}[{LayoutName}]: cellText = `{CellText}`", _pageName, _layoutName, cellText);
        
        return cellText;
    }
}

public enum WorkerTableHeader
{
    [StringValue("First Name")]
    FirstName,
    
    [StringValue("Last Name")]
    LastName,
    
    [StringValue("Age")]
    Age,
    
    [StringValue("Email")]
    Email,
    
    [StringValue("Salary")]
    Salary,
    
    [StringValue("Department")]
    Department
}