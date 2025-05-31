using Allure.NUnit.Attributes;
using Bogus;
using Karpatium.Core.Nunit;
using Karpatium.Core.Web;
using ToolsQa.UI;
using ToolsQa.UI.Dto;
using ToolsQa.UI.Layouts;

namespace ToolsQa.Tests.TestCases;

[AllureParentSuite("Tools QA")]
[AllureSuite("Elements")]
[AllureSubSuite("Web Tables")]
[TestFixture]
public sealed class WebTablesTests : BaseFixture<EmptyTestData>
{
    protected override string TestDataPath => string.Empty;
    
    [AllureBefore]
    [SetUp]
    public void SetUp()
    {
        WebManager.Browser.NavigateTo($"{TestConfiguration.ApplicationSettings.BaseUrl}{ToolsQaPageUrls.WebTablesPage}");
        RemoveBanners();
    }

    [TestCase(TestName = "Verify that adding worker is possible.")]
    [RetryOnErrorAndFailure(3)]
    public void VerifyThatAddingWorkerIsPossible()
    {
        WorkerDto worker = GetRandomWorker();
        
        ToolsQaPages.WebTablesPage.ClickAdd();
        ToolsQaPages.WebTablesPage.RegistrationFormModal.FillForm(worker);
        ToolsQaPages.WebTablesPage.RegistrationFormModal.ClickSubmit();

        WorkerTableRowItem row = ToolsQaPages.WebTablesPage.WorkerTable.GetRow(WorkerTableHeader.Email, worker.Email) 
                                   ?? throw new Exception($"Row with `{worker.Email}` email not found.");
        string actualFirstName = row.GetCellText(WorkerTableHeader.FirstName);
        string actualLastName = row.GetCellText(WorkerTableHeader.LastName);
        string actualAge = row.GetCellText(WorkerTableHeader.Age);
        string actualEmail = row.GetCellText(WorkerTableHeader.Email);
        string actualSalary = row.GetCellText(WorkerTableHeader.Salary);
        string actualDepartment = row.GetCellText(WorkerTableHeader.Department);
        WorkerDto actualWorker = new WorkerDto()
        {
            FirstName = actualFirstName,
            LastName = actualLastName,
            Age = actualAge,
            Email = actualEmail,
            Salary = actualSalary,
            Department = actualDepartment
        };
        Assert.That(actualWorker, Is.EqualTo(worker), "Incorrect department.");
    }

    [TestCase(TestName = "Verify that deleting worker is possible.")]
    [RetryOnErrorAndFailure(3)]
    public void VerifyThatDeletingWorkerIsPossible()
    {
        WorkerDto worker = GetRandomWorker();
        
        ToolsQaPages.WebTablesPage.ClickAdd();
        ToolsQaPages.WebTablesPage.RegistrationFormModal.FillForm(worker);
        ToolsQaPages.WebTablesPage.RegistrationFormModal.ClickSubmit();

        WorkerTableRowItem? row = ToolsQaPages.WebTablesPage.WorkerTable.GetRow(WorkerTableHeader.Email, worker.Email) 
                                    ?? throw new Exception($"Row with `{worker.Email}` email not found.");
        row.ClickDelete();

        row = ToolsQaPages.WebTablesPage.WorkerTable.GetRow(WorkerTableHeader.Email, worker.Email);
        Assert.That(row, Is.Null, $"Row with `{worker.Email}` email is not deleted.");
    }
    
    [TestCase(TestName = "Verify that editing worker is possible.")]
    [RetryOnErrorAndFailure(3)]
    public void VerifyThatEditingWorkerIsPossible()
    {
        WorkerDto worker = GetRandomWorker();
        
        ToolsQaPages.WebTablesPage.ClickAdd();
        ToolsQaPages.WebTablesPage.RegistrationFormModal.FillForm(worker);
        ToolsQaPages.WebTablesPage.RegistrationFormModal.ClickSubmit();

        WorkerTableRowItem row = ToolsQaPages.WebTablesPage.WorkerTable.GetRow(WorkerTableHeader.Email, worker.Email) 
                              ?? throw new Exception($"Row with `{worker.Email}` email not found.");
        row.ClickEdit();
        
        worker = GetRandomWorker();
        ToolsQaPages.WebTablesPage.RegistrationFormModal.FillForm(worker);
        ToolsQaPages.WebTablesPage.RegistrationFormModal.ClickSubmit();

        row = ToolsQaPages.WebTablesPage.WorkerTable.GetRow(WorkerTableHeader.Email, worker.Email) 
              ?? throw new Exception($"Row with `{worker.Email}` email not found.");
        string actualFirstName = row.GetCellText(WorkerTableHeader.FirstName);
        string actualLastName = row.GetCellText(WorkerTableHeader.LastName);
        string actualAge = row.GetCellText(WorkerTableHeader.Age);
        string actualEmail = row.GetCellText(WorkerTableHeader.Email);
        string actualSalary = row.GetCellText(WorkerTableHeader.Salary);
        string actualDepartment = row.GetCellText(WorkerTableHeader.Department);
        WorkerDto actualWorker = new WorkerDto()
        {
            FirstName = actualFirstName,
            LastName = actualLastName,
            Age = actualAge,
            Email = actualEmail,
            Salary = actualSalary,
            Department = actualDepartment
        };
        Assert.That(actualWorker, Is.EqualTo(worker), "Incorrect department.");
    }

    private WorkerDto GetRandomWorker()
    {
        WorkerDto worker = new Faker<WorkerDto>()
            .StrictMode(true)
            .RuleFor(w => w.FirstName, f => f.Name.FirstName())
            .RuleFor(w => w.LastName, f => f.Name.LastName())
            .RuleFor(w => w.Email, (f, w) => f.Internet.Email(w.FirstName, w.LastName))
            .RuleFor(w => w.Age, f => f.Random.Number(18, 65).ToString())
            .RuleFor(w => w.Salary, f => f.Random.Number(10_000, 100_000).ToString())
            .RuleFor(w => w.Department, f => f.PickRandom(new[] { "HR", "Finance", "Engineering", "Marketing", "Sales" }))
            .Generate();

        return worker;
    }
}