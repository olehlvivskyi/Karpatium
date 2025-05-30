using Karpatium.Core.Attributes;
using Karpatium.Core.Extensions;
using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;
using ToolsQa.UI.Components.React;
using ToolsQa.UI.Pages;

namespace ToolsQa.UI.Layouts;

/// <summary>
/// Provides access to the "Worker" table.
/// </summary>
public class WorkerTableLayout
{
    private const string PageName = nameof(WebTablesPage);
    private const string TableName = nameof(WorkerTableLayout);
    
    private readonly ReactTableComponent _tableComponent;

    internal WorkerTableLayout(Element tableWrapper)
    {
        _tableComponent = new(tableWrapper);
    }
    
    /// <summary>
    /// Gets a specific row where the cell value under a given header matches the specified text.
    /// </summary>
    /// <param name="header">The header text of the column to search in.</param>
    /// <param name="cellText">The cell text to match under the specified header column.</param>
    public WorkerTableRowLayout? GetRow(WorkerTableHeader header, string cellText)
    {
        Log.Information("{PageName}-{TableName}: Checking row where `{HeaderText}` header has `{CellText}` cell text.", PageName, TableName, header, cellText);

        string headerText = header.GetStringValue();
        ReactTableRowComponent? row = _tableComponent.GetRow(headerText, cellText);
        Log.Debug("{PageName}-{TableName}: row = {Row}", PageName, TableName, row == null ? "null" : "not null");
        
        return row == null 
            ? null 
            : new WorkerTableRowLayout(row);
    }
}

/// <summary>
/// Provides access to the "Worker" table row.
/// </summary>
public class WorkerTableRowLayout
{
    private const string PageName = nameof(WebTablesPage);
    private const string TableName = nameof(WorkerTableLayout);
    
    private readonly ReactTableRowComponent _rowComponent;
    
    internal WorkerTableRowLayout(ReactTableRowComponent rowComponent)
    {
        _rowComponent = rowComponent;
    }
    
    private CommonElement Delete => ElementFactory.Create<CommonElement>(Selector.Css("[title='Delete']"), _rowComponent.ComponentWrapper);
    private CommonElement Edit => ElementFactory.Create<CommonElement>(Selector.Css("[title='Edit']"), _rowComponent.ComponentWrapper);

    /// <summary>
    /// Clicks on the "Delete" icon.
    /// </summary>
    public void ClickDelete()
    {
        Log.Information("{PageName}-{TableName}: Clicking on the \"Delete\" icon.", PageName, TableName);
        
        Delete.Click();
    }

    /// <summary>
    /// Clicks on the "Edit" icon.
    /// </summary>
    public void ClickEdit()
    {
        Log.Information("{PageName}-{TableName}: Clicking on the \"Edit\" icon.", PageName, TableName);
        
        Edit.Click();
    }

    /// <summary>
    /// Gets the text content of a cell in the row based on the specified header.
    /// </summary>
    /// <param name="header">The header of the column whose cell text is to be retrieved.</param>
    public string GetCellText(WorkerTableHeader header)
    {
        Log.Information("{PageName}-{TableName}: Checking cell text for \"{Header}\" header.", PageName, TableName, header);
        
        string headerText = header.GetStringValue();
        string cellText = _rowComponent.GetCellText(headerText);
        Log.Debug("{PageName}-{TableName}: cellText = `{CellText}`", PageName, TableName, cellText);
        
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