using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;
using ToolsQa.UI.Dto;

namespace ToolsQa.UI.Dialogs;

/// <summary>
/// Provides access to the "Registration Form" dialog.
/// </summary>
public sealed class RegistrationFormDialog(CommonElement dialogWrapper, string pageName) : BaseDialog<CommonElement>(dialogWrapper, pageName)
{
    protected override string DialogName => "\"Registration Form\" dialog";

    private InputElement Age => ElementFactory.Create<InputElement>(Selector.Id("age"), DialogWrapper);
    private InputElement Department => ElementFactory.Create<InputElement>(Selector.Id("department"), DialogWrapper);
    private InputElement Email => ElementFactory.Create<InputElement>(Selector.Id("userEmail"), DialogWrapper);
    private InputElement FirstName => ElementFactory.Create<InputElement>(Selector.Id("firstName"), DialogWrapper);
    private InputElement LastName => ElementFactory.Create<InputElement>(Selector.Id("lastName"), DialogWrapper);
    private InputElement Salary => ElementFactory.Create<InputElement>(Selector.Id("salary"), DialogWrapper);
    private CommonElement Submit => ElementFactory.Create<CommonElement>(Selector.Id("submit"), DialogWrapper);

    /// <summary>
    /// Clicks on the "Submit" button.
    /// </summary>
    public void ClickSubmit()
    {
        Log.Information("{PageName}[{DialogName}]: Clicking on the \"Submit\" button.", PageName, DialogName);
        
        Submit.Click();
    }

    /// <summary>
    /// Sets the value for the "Age" field.
    /// </summary>
    /// <param name="age">The text to set as the age.</param>
    public void SetAge(string age)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{Age}` for \"Age\" field.", PageName, DialogName, age);
        
        Age.SetText(age);
    }

    /// <summary>
    /// Sets the value for the "Department" field.
    /// </summary>
    /// <param name="department">The text to set as the department.</param>
    public void SetDepartment(string department)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{Department}` for \"Department\" field.", PageName, DialogName, department);
        
        Department.SetText(department);
    }

    /// <summary>
    /// Sets the value for the "Email" field.
    /// </summary>
    /// <param name="email">The text to set as the email.</param>
    public void SetEmail(string email)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{Email}` for \"Email\" field.", PageName, DialogName, email);
        
        Email.SetText(email);
    }
    
    /// <summary>
    /// Sets value for "First Name" field.
    /// </summary>
    /// <param name="firstName">The text to set as the first name.</param>
    public void SetFirstName(string firstName)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{FirstName}` for \"First Name\" field.", PageName, DialogName, firstName);
        
        FirstName.SetText(firstName);
    }

    /// <summary>
    /// Sets value for "Last Name" field.
    /// </summary>
    /// <param name="lastName">The text to set as the last name.</param>
    public void SetLastName(string lastName)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{LastName}` for \"Last Name\" field.", PageName, DialogName, lastName);
        
        LastName.SetText(lastName);
    }

    /// <summary>
    /// Sets the value for the "Salary" field.
    /// </summary>
    /// <param name="salary">The text to set as the salary.</param>
    public void SetSalary(string salary)
    {
        Log.Information("{PageName}[{DialogName}]: Setting `{Salary}` for \"Salary\" field.", PageName, DialogName, salary);
        
        Salary.SetText(salary);
    }

    /// <summary>
    /// Fills out the registration form using the provided worker data.
    /// </summary>
    /// <param name="workerDto">The data transfer object containing the worker's information to populate the form fields.</param>
    public void FillForm(WorkerDto workerDto)
    {
        SetFirstName(workerDto.FirstName);
        SetLastName(workerDto.LastName);
        SetEmail(workerDto.Email);
        SetAge(workerDto.Age);
        SetSalary(workerDto.Salary);
        SetDepartment(workerDto.Department);
    }
}