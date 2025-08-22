using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;
using ToolsQa.UI.Dialogs;
using ToolsQa.UI.Layouts;

namespace ToolsQa.UI.Pages;

/// <summary>
/// Provides access to the "Web Tables" page.
/// </summary>
public class WebTablesPage(string relativePath) : BasePage(relativePath)
{
    protected override string PageName => "\"Web Tables\" page";
    
    private CommonElement Add => ElementFactory.Create<CommonElement>(Selector.Id("addNewRecordButton"));
    private CommonElement RegistrationFormDialogWrapper => ElementFactory.Create<CommonElement>(Selector.Class("modal-content"));
    private CommonElement WorkerTableWrapper => ElementFactory.Create<CommonElement>(Selector.Class("ReactTable"));

    /// <summary>
    /// Provides access to the "Registration Form" dialog.
    /// </summary>
    public RegistrationFormDialog RegistrationFormDialog => new(RegistrationFormDialogWrapper, PageName);
    
    /// <summary>
    /// Provides access to the "Worker" table.
    /// </summary>
    public WorkerTableLayout WorkerTable => new(WorkerTableWrapper, PageName);

    /// <summary>
    /// Clicks on the "Add" button.
    /// </summary>
    public void ClickAdd()
    {
        Log.Information("{PageName}: Clicking on the \"Add\" button.", PageName);
        
        Add.Click();
    }
}