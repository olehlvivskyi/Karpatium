using Karpatium.Core.Web;
using Karpatium.Core.Web.Elements;
using Serilog;
using ToolsQa.UI.Dto;

namespace ToolsQa.UI.Modals;

/// <summary>
/// Provides access to the "Registration Form" modal.
/// </summary>
public class RegistrationFormModal(Element modalWrapper, string pageName) : BaseModal(modalWrapper, pageName)
{
    private const string ModalName = nameof(RegistrationFormModal);

    private InputElement Age => ElementFactory.Create<InputElement>(Selector.Id("age"), ModalWrapper);
    private InputElement Department => ElementFactory.Create<InputElement>(Selector.Id("department"), ModalWrapper);
    private InputElement Email => ElementFactory.Create<InputElement>(Selector.Id("userEmail"), ModalWrapper);
    private InputElement FirstName => ElementFactory.Create<InputElement>(Selector.Id("firstName"), ModalWrapper);
    private InputElement LastName => ElementFactory.Create<InputElement>(Selector.Id("lastName"), ModalWrapper);
    private InputElement Salary => ElementFactory.Create<InputElement>(Selector.Id("salary"), ModalWrapper);
    private CommonElement Submit => ElementFactory.Create<CommonElement>(Selector.Id("submit"), ModalWrapper);

    /// <summary>
    /// Clicks on the "Submit" button.
    /// </summary>
    public void ClickSubmit()
    {
        Log.Information("{PageName}-{ModalName}: Clicking on the \"Submit\" button.", PageName, ModalName);
        
        Submit.Click();
    }

    /// <summary>
    /// Sets the value for the "Age" field.
    /// </summary>
    /// <param name="age">The text to set as the age.</param>
    public void SetAge(string age)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{Age}` for \"Age\" field.", PageName, ModalName, age);
        
        Age.SetText(age);
    }

    /// <summary>
    /// Sets the value for the "Department" field.
    /// </summary>
    /// <param name="department">The text to set as the department.</param>
    public void SetDepartment(string department)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{Department}` for \"Department\" field.", PageName, ModalName, department);
        
        Department.SetText(department);
    }

    /// <summary>
    /// Sets the value for the "Email" field.
    /// </summary>
    /// <param name="email">The text to set as the email.</param>
    public void SetEmail(string email)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{Email}` for \"Email\" field.", PageName, ModalName, email);
        
        Email.SetText(email);
    }
    
    /// <summary>
    /// Sets value for "First Name" field.
    /// </summary>
    /// <param name="firstName">The text to set as the first name.</param>
    public void SetFirstName(string firstName)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{FirstName}` for \"First Name\" field.", PageName, ModalName, firstName);
        
        FirstName.SetText(firstName);
    }

    /// <summary>
    /// Sets value for "Last Name" field.
    /// </summary>
    /// <param name="lastName">The text to set as the last name.</param>
    public void SetLastName(string lastName)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{LastName}` for \"Last Name\" field.", PageName, ModalName, lastName);
        
        LastName.SetText(lastName);
    }

    /// <summary>
    /// Sets the value for the "Salary" field.
    /// </summary>
    /// <param name="salary">The text to set as the salary.</param>
    public void SetSalary(string salary)
    {
        Log.Information("{PageName}-{ModalName}: Setting `{Salary}` for \"Salary\" field.", PageName, ModalName, salary);
        
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